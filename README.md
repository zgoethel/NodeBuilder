# Node Builder
UI tool for creating grammars and generating parsers

This project aims to be a frontend to a set of parsing utilities. It implements common patterns used by expression trees and statements. These patterns can be composed into more complex grammars, and the resulting language parser should be reusable in any (compatible) .NET project.

UI editing features allow simple interactive design of grammar elements.

# `2025-03-13`

The grammar design frontend uses a blend of Windows Forms dialogs and WebView2 panes hosting Blazor components.

Nodes are draggable in the graph editing pane. The pane itself is scalable and pannable. Nodes are drawn as HTML content placed absolutely in the graph editing pane. SVG arcs are drawn from node handle to node handle by querying the DOM for the client positions of node handle anchors.

Grammar tokens will eventually live in the "Tokens" form. For now, there is a regex editing and testing modal built on the included regular expression implementation.

![Interactive nodes and base forms for describing token sets](https://i.imgur.com/UP4UlW3.png)

_Interactive nodes and base forms for describing token sets_

# `2025-03-02`

## Stack Usage

Parsing execution is trampolined and an execution queue and stack are maintained in the heap.

Each recursive call is able to produce two types of future work,
 * "work"&mdash;additional parsing work which will be executed FIFO eventually after exiting
   - A "work" task can queue nested "work" or "tail" tasks
   - `addWork` returns a handle which contains a `Result` value
 * "tail"&mdash;additional parsing work which will be executed FILO after all "work" tasks are exhausted
   - A "tail" task can queue nested "work" or "tail" tasks
   - Values returned by the last-executed `addTail` task will be propagated to as the parent task's `Result` value.

 and is implemented as a task of the delegate type `Trampoline.WorkUnit`. There is no guarantee the `Result` value of the handle returned by `addWork` is available unless executing within an `addTail` following that `addWork`.

 ```csharp
Trampoline.WorkUnit Function(Trampoline.WorkUnit subFunction)
{
    return (addWork, addTail) =>
    {
        var subValue = addWork(subFunction);

        // subValue.Result is not ready yet

        addWork((addWork, addTail) =>
        {
            // Execution order of `addWork` calls is guaranteed; however,
            // subValue.Result may not be ready yet

            return null;
        });

        addTail((addWork, addTail) =>
        {
            // subValue.Result should be ready now

            // Propagate sub-value as return value of parent task
            return subValue.Result;
        });

        return null;
    };
}
 ```

The `Trampoline` continuously loops and executes one task at a time,
 * from the FIFO "work" queue if any tasks are present
 * from the "tail" stack if no FIFO tasks are present but a task is on the stack; popping from this stack represents a function completing and returning a result

Any object returned from the last-executed task added via `addTail` will be propagated to the nearest containing `addWork` as its `Result` value; if there is no such parent, the result will be propagated all the way and be returned by `Trampoline.Execute`.

If a `Trampoline.WorkUnit` never calls `addTail`, the returned value from the `Trampoline.WorkUnit` delegate will be used as `Result`.

```csharp
Trampoline.WorkUnit SubFunction(string text)
{
    return (addWork, addTail) =>
    {
        // addWork(...);

        // Result value used if `addTail` is never called
        return text;
    };
}
```

Compose the two functions and execute them on the trampoline to yield `"Hello, world!"`.

```csharp
var subFunction = SubFunction("Hello, world!");
var function = Function(subFunction);

var result = await Trampoline.Execute(function, CancellationToken.None);

Console.WriteLine(result);
```

## Example of Direct Parsing Implementation
The following calls would be designed and assembled in the background, controlled by higher-level user input. This demo references the backend implementation directly to illustrate the parsing approach.

Build a set of tokens.
```csharp
var fsa = new Fsa();

fsa.Build("[0-9]+", (int)_Token.Number);
fsa.Build("\\+", (int)_Token.Add);
fsa.Build("\\-\\>", (int)_Token.Deref);
fsa.Build("\\-", (int)_Token.Subtract);
fsa.Build("\\*", (int)_Token.Multiply);
fsa.Build("\\/", (int)_Token.Divide);
fsa.Build("\\.", (int)_Token.Access);
fsa.Build("\\,", (int)_Token.Comma);
fsa.Build("\\^", (int)_Token.Exponent);
fsa.Build("\\(", (int)_Token.OpenParens);
fsa.Build("\\)", (int)_Token.CloseParens);
fsa.Build("\\!", (int)_Token.Not);
fsa.Build("[ \n\r\t]+", 9999);

fsa = fsa.ConvertToDfa().MinimizeDfa();
```

Prepare the input text.
```csharp
var source = "((1 + 2).3.4(40, 41, 42)->5) * !!6->7 / (8^9)^10.11^(12)";
var stream = new TokenStream(fsa, source);
```

Build a grammar, defined as composed anonymous functions of type `Trampoline.WorkUnit`.
```csharp
// Grammar top-level, which will be available later
Trampoline.WorkUnit? _expr = null;
object? expr(Trampoline.WorkBuilder a, Trampoline.WorkBuilder b)
{
    return _expr?.Invoke(a, b);
}

var literal = Production.Literal(
    [
        (int)_Token.Number
    ]);

var parens = Production.Body(
    startToken: (int)_Token.OpenParens,
    endToken: (int)_Token.CloseParens,
    content: expr);

var member = Production.FirstSet(
    fallback: null,
    ahead: 0,
    ((int)_Token.OpenParens, parens),
    ((int)_Token.Number, literal));

var invokeParameterList = Production.Body(
    startToken: (int)_Token.OpenParens,
    endToken: (int)_Token.CloseParens,
    content: Production.InfixPostfixOperator(
        [
            ((int)_Token.Comma, true, null)
        ],
        expr));

var exprA = Production.InfixPostfixOperator(
    [
        ((int)_Token.Deref, true, null),
        ((int)_Token.Access, true, null),
        ((int)_Token.OpenParens, false, invokeParameterList)
    ],
    nextPrecedence: member);

var exprB = Production.InfixPostfixOperator(
    [
        ((int)_Token.Exponent, true, null)
    ],
    nextPrecedence: exprA,
    assoc: SD.Associativity.Right);

var exprC = Production.PrefixOperator(
    [
        ((int)_Token.Not, null)
    ],
    nextPrecedence: exprB);

var exprD = Production.InfixPostfixOperator(
    [
        ((int)_Token.Multiply, true, null),
        ((int)_Token.Divide, true, null)
    ],
    nextPrecedence: exprC);

var exprE = Production.InfixPostfixOperator(
    [
        ((int)_Token.Add, true, null),
        ((int)_Token.Subtract, true, null)
    ],
    nextPrecedence: exprD);

// Make grammar top-level available
_expr = exprE;
```

Then execute the parser.
```csharp
var parserOutput = await ParserContext.Begin(
    stream,
    async () => await Trampoline.Execute(expr, CancellationToken.None));
```

The original input text
```
((1 + 2).3.4(40, 41, 42)->5) * !!6->7 / (8^9)^10.11^(12)
```

produces the following `parserOutput` (whose tree structure mirrors the precedence rules of the built expression grammar):
<details> 
  <summary><b>JSON of resulting syntax tree</b></summary>

```json
{
  "Assoc": "Left",
  "Members": [
    {
      "OpToken": 0,
      "OpText": "",
      "Value": {
        "Assoc": "Left",
        "Members": [
          {
            "OpToken": 0,
            "OpText": "",
            "Value": {
              "Assoc": "Left",
              "Members": [
                {
                  "OpToken": 0,
                  "OpText": "",
                  "Value": {
                    "Token": 1,
                    "Text": "1"
                  },
                  "Data": null
                },
                {
                  "OpToken": 2,
                  "OpText": "+",
                  "Value": {
                    "Token": 1,
                    "Text": "2"
                  },
                  "Data": null
                }
              ]
            },
            "Data": null
          },
          {
            "OpToken": 7,
            "OpText": ".",
            "Value": {
              "Token": 1,
              "Text": "3"
            },
            "Data": null
          },
          {
            "OpToken": 7,
            "OpText": ".",
            "Value": {
              "Token": 1,
              "Text": "4"
            },
            "Data": null
          },
          {
            "OpToken": 10,
            "OpText": "(",
            "Value": null,
            "Data": {
              "Assoc": "Left",
              "Members": [
                {
                  "OpToken": 0,
                  "OpText": "",
                  "Value": {
                    "Token": 1,
                    "Text": "40"
                  },
                  "Data": null
                },
                {
                  "OpToken": 8,
                  "OpText": ",",
                  "Value": {
                    "Token": 1,
                    "Text": "41"
                  },
                  "Data": null
                },
                {
                  "OpToken": 8,
                  "OpText": ",",
                  "Value": {
                    "Token": 1,
                    "Text": "42"
                  },
                  "Data": null
                }
              ]
            }
          },
          {
            "OpToken": 6,
            "OpText": "->",
            "Value": {
              "Token": 1,
              "Text": "5"
            },
            "Data": null
          }
        ]
      },
      "Data": null
    },
    {
      "OpToken": 4,
      "OpText": "*",
      "Value": {
        "OpToken": 12,
        "OpText": "!",
        "Value": {
          "OpToken": 12,
          "OpText": "!",
          "Value": {
            "Assoc": "Left",
            "Members": [
              {
                "OpToken": 0,
                "OpText": "",
                "Value": {
                  "Token": 1,
                  "Text": "6"
                },
                "Data": null
              },
              {
                "OpToken": 6,
                "OpText": "->",
                "Value": {
                  "Token": 1,
                  "Text": "7"
                },
                "Data": null
              }
            ]
          },
          "Data": null
        },
        "Data": null
      },
      "Data": null
    },
    {
      "OpToken": 5,
      "OpText": "/",
      "Value": {
        "Assoc": "Right",
        "Members": [
          {
            "OpToken": 0,
            "OpText": "",
            "Value": {
              "Assoc": "Right",
              "Members": [
                {
                  "OpToken": 0,
                  "OpText": "",
                  "Value": {
                    "Token": 1,
                    "Text": "8"
                  },
                  "Data": null
                },
                {
                  "OpToken": 9,
                  "OpText": "^",
                  "Value": {
                    "Token": 1,
                    "Text": "9"
                  },
                  "Data": null
                }
              ]
            },
            "Data": null
          },
          {
            "OpToken": 9,
            "OpText": "^",
            "Value": {
              "Assoc": "Left",
              "Members": [
                {
                  "OpToken": 0,
                  "OpText": "",
                  "Value": {
                    "Token": 1,
                    "Text": "10"
                  },
                  "Data": null
                },
                {
                  "OpToken": 7,
                  "OpText": ".",
                  "Value": {
                    "Token": 1,
                    "Text": "11"
                  },
                  "Data": null
                }
              ]
            },
            "Data": null
          },
          {
            "OpToken": 9,
            "OpText": "^",
            "Value": {
              "Token": 1,
              "Text": "12"
            },
            "Data": null
          }
        ]
      },
      "Data": null
    }
  ]
}
```

</details>