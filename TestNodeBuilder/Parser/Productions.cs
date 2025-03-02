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

    public class BinaryOperatorDto
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

    public static Trampoline.WorkUnit InfixOperator(int[] opTokens, Trampoline.WorkUnit nextPrecedence, SD.Associativity assoc = SD.Associativity.Left)
    {
        return (addWork, addTail) =>
        {
            var result = new BinaryOperatorDto()
            {
                Assoc = assoc
            };

            var left = addWork(nextPrecedence);

            addWork((addWork, addTail) =>
            {
                if (left.Result is null)
                {
                    //TODO Emit error
                } else
                {
                    result.Members.Add(new()
                    {
                        Value = left.Result
                    });
                }

                return null;
            });

            object? rightTail(Trampoline.WorkBuilder addWork, Trampoline.WorkBuilder addTail)
            {
                if (!opTokens.Contains(ParserContext.TokenStream.Next))
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

                var right = addWork(nextPrecedence);

                addTail(rightTail);
                addTail((addWork, addTail) =>
                {
                    if (right.Result is null)
                    {
                        //TODO Emit error
                    }

                    result.Members.Add(new()
                    {
                        OpText = text,
                        OpToken = token,
                        Value = right.Result
                    });

                    return null;
                });

                return null;
            };

            addTail(rightTail);

            return null;
        };
    }
}
