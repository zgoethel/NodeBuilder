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

    public static Trampoline.WorkUnit BinaryOperator(int[] opTokens, Trampoline.WorkUnit nextPrecedence, SD.Associativity assoc = SD.Associativity.Left)
    {
        return (addWork, addTail) =>
        {
            var result = new BinaryOperatorDto()
            {
                Assoc = assoc
            };

            void pushOperator()
            {
                var opToken = addTail((addWork, addTail) =>
                {
                    if (!opTokens.Contains(ParserContext.TokenStream.Next))
                    {
                        return null;
                    }

                    var token = ParserContext.TokenStream.Poll();
                    var text = ParserContext.TokenStream.Text;

                    addWork((addWork, addTail) =>
                    {
                        var right = nextPrecedence(addWork, addTail);
                        if (right is null)
                        {
                            //TODO Emit error
                            return null;
                        }

                        pushOperator();

                        result.Members.Add(new()
                        {
                            OpToken = token,
                            OpText = text,
                            Value = right
                        });
                        return right;
                    });

                    return null;
                });
            }

            addWork((addWork, addTail) =>
            {
                var left = nextPrecedence(addWork, addTail);
                if (left is null)
                {
                    return null;
                }

                pushOperator();

                result.Members.Add(new()
                {
                    Value = left
                });
                return left;
            });

            return result;
        };
    }
}
