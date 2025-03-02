using TestNodeBuilder.Models;

namespace TestNodeBuilder.Parser;

public static class Production
{
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

    public class InfixPostfixOperatorDto
    {
        public class Member
        {
            public int OpToken { get; set; }

            public string OpText { get; set; } = "";

            public object? Value { get; set; }
        }

        public SD.Associativity Assoc { get; set; } = SD.Associativity.Left;

        public List<Member> Members { get; set; } = [];
    }

    public static Trampoline.WorkUnit InfixPostfixOperator((int t, bool r)[] opTokens, Trampoline.WorkUnit nextPrecedence, SD.Associativity assoc = SD.Associativity.Left)
    {
        return (addWork, addTail) =>
        {
            var result = new InfixPostfixOperatorDto()
            {
                Assoc = assoc
            };

            var left = addWork(nextPrecedence);
            var first = true;

            object? rightTail(Trampoline.WorkBuilder addWork, Trampoline.WorkBuilder addTail)
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

                var token = ParserContext.TokenStream.Poll();
                var text = ParserContext.TokenStream.Text;

                Trampoline.WorkUnitResult? right = null;
                if (op.r)
                {
                    right = addWork(nextPrecedence);
                }

                addTail(rightTail);
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
                        Value = right?.Result
                    });

                    return null;
                });

                return null;
            };

            addTail(rightTail);

            return null;
        };
    }

    public static Trampoline.WorkUnit FirstSet(Trampoline.WorkUnit? fallback, params (int, Trampoline.WorkUnit)[] options)
    {
        return (addWork, addTail) =>
        {
            var token = ParserContext.TokenStream.Next;
            var matchingUnit = options.FirstOrDefault((it) => it.Item1 == token).Item2 ?? fallback;

            if (matchingUnit is null)
            {
                //TODO Emit error
                return null;
            } else
            {
                return matchingUnit(addWork, addTail);
            }
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
