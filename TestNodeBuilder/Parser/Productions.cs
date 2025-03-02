using TestNodeBuilder.Models;

namespace TestNodeBuilder.Parser;

public static class Production
{
    public static Trampoline.WorkUnit FirstSet(Trampoline.WorkUnit? fallback, int ahead, params (int t, Trampoline.WorkUnit w)[] options)
    {
        return (addWork, addTail) =>
        {
            var originalOffset = ParserContext.TokenStream.Offset;
            try
            {
                for (var i = 0; i < ahead; i++)
                {
                    ParserContext.TokenStream.Poll();
                }

                var token = ParserContext.TokenStream.Next;
                var matchingUnit = options.FirstOrDefault((it) => it.t == token).w ?? fallback;

                if (matchingUnit is null)
                {
                    //TODO Emit error
                    return null;
                } else
                {
                    var result = addWork(matchingUnit);

                    addTail((addWork, addTail) =>
                    {
                        return result.Result;
                    });
                }
            } finally
            {
                if (ParserContext.TokenStream.Offset != originalOffset)
                {
                    ParserContext.TokenStream.Seek(originalOffset);
                }
            }

            return null;
        };
    }

    public class LiteralDto
    {
        public int Token { get; set; }

        public string Text { get; set; } = "";
    }

    public static Trampoline.WorkUnit Literal(int[] litTokens)
    {
        return (addWork, addTail) =>
        {
            if (!litTokens.Contains(ParserContext.TokenStream.Next))
            {
                //TODO Emit error
                return null;
            }

            return new LiteralDto()
            {
                Token = ParserContext.TokenStream.Poll(),
                Text = ParserContext.TokenStream.Text
            };
        };
    }

    public class PrefixOperatorDto
    {
        public int OpToken { get; set; }

        public string OpText { get; set; } = "";

        public object? Value { get; set; }
    }

    public static Trampoline.WorkUnit PrefixOperator(int[] opTokens, Trampoline.WorkUnit nextPrecedence)
    {
        return (addWork, addTail) =>
        {
            if (!opTokens.Contains(ParserContext.TokenStream.Next))
            {
                var value = addWork(nextPrecedence);

                addTail((addWork, addTail) =>
                {
                    return value.Result;
                });

                return null;
            }

            var token = ParserContext.TokenStream.Poll();
            var text = ParserContext.TokenStream.Text;

            var nextPrefix = addWork(PrefixOperator(opTokens, nextPrecedence));

            addTail((addWork, addTail) =>
            {
                if (nextPrefix.Result is null)
                {
                    //TODO Emit error
                }
                return new PrefixOperatorDto()
                {
                    OpToken = token,
                    OpText = text,
                    Value = nextPrefix.Result
                };
            });

            return null;
        };
    }

    public class InfixPostfixOperatorDto
    {
        public class Member
        {
            public int OpToken { get; set; }

            public string OpText { get; set; } = "";

            public object? Value { get; set; }

            public object? Data { get; set; }
        }

        public SD.Associativity Assoc { get; set; } = SD.Associativity.Left;

        public List<Member> Members { get; set; } = [];
    }

    public static Trampoline.WorkUnit InfixPostfixOperator((int t, bool r, Trampoline.WorkUnit? w)[] opTokens, Trampoline.WorkUnit nextPrecedence, SD.Associativity assoc = SD.Associativity.Left)
    {
        return (addWork, addTail) =>
        {
            var result = new InfixPostfixOperatorDto()
            {
                Assoc = assoc
            };

            var left = addWork(nextPrecedence);
            var first = true;

            object? opTail(Trampoline.WorkBuilder addWork, Trampoline.WorkBuilder addTail)
            {
                if (first)
                {
                    if (left.Result is null)
                    {
                        //TODO Emit error
                    }
                    result.Members.Add(new()
                    {
                        Value = left.Result
                    });
                }
                first = false;

                var op = opTokens.FirstOrDefault((it) => it.t == ParserContext.TokenStream.Next);
                if (op.t == 0)
                {
                    return result.Members.Count switch
                    {
                        0 => null,
                        1 => result.Members.Single().Value,
                        _ => result
                    };
                }

                var token = ParserContext.TokenStream.Next;
                var text = ParserContext.TokenStream.Text;

                object? opData = null;

                // Executed last (outer)
                // Effective return value (outer)
                addTail((addWork, addTail) =>
                {
                    Trampoline.WorkUnitResult? right = null;
                    if (op.r)
                    {
                        right = addWork(nextPrecedence);
                    }

                    // Executed last (inner)
                    addTail(opTail); // Effective return value (inner)

                    // Executed first (inner)
                    addTail((addWork, addTail) =>
                    {
                        if (op.r && right!.Result is null)
                        {
                            //TODO Emit error
                        }
                        result.Members.Add(new()
                        {
                            OpText = text,
                            OpToken = token,
                            Value = right?.Result,
                            Data = opData
                        });

                        return null;
                    });

                    return null;
                });

                // Executed first (outer)
                if (op.w is null)
                {
                    ParserContext.TokenStream.Poll();
                } else
                {
                    var opResult = addWork(op.w!);

                    addTail((addWork, addTail) =>
                    {
                        opData = opResult.Result;

                        return null;
                    });
                }

                return null;
            };

            addTail(opTail);

            return null;
        };
    }

    public static Trampoline.WorkUnit Body(int startToken, int endToken, Trampoline.WorkUnit content)
    {
        return (addWork, addTail) =>
        {
            if (ParserContext.TokenStream.Next != startToken)
            {
                //TODO Emit error
            } else
            {
                ParserContext.TokenStream.Poll();
            }

            var contentResult = addWork(content);

            addTail((addWork, addTail) =>
            {
                if (contentResult.Result is null)
                {
                    //TODO Emit error
                }

                if (ParserContext.TokenStream.Next != endToken)
                {
                    //TODO Emit error
                } else
                {
                    ParserContext.TokenStream.Poll();
                }

                return contentResult.Result;
            });

            return null;
        };
    }
}
