using Microsoft.AspNetCore.Components;

namespace TestNodeBuilder;

public static class SD
{
    public enum Associativity
    {
        Left = 1,
        Right
    }

    // https://icons.getbootstrap.com/
    public static class Icons
    {
        public const decimal DEFAULT_SIZE = 16.0m;

        public static MarkupString BiArrowRightShort(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-arrow-right-short"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M4 8a.5.5 0 0 1 .5-.5h5.793L8.146 5.354a.5.5 0 1 1 .708-.708l3 3a.5.5 0 0 1 0 .708l-3 3a.5.5 0 0 1-.708-.708L10.293 8.5H4.5A.5.5 0 0 1 4 8"" />
            </svg>
            "
            );

        public static MarkupString BiBack(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-back"" viewBox=""0 0 16 16"">
                <path d=""M0 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v2h2a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2v-2H2a2 2 0 0 1-2-2zm2-1a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1z""/>
            </svg>
            "
            );

        public static MarkupString BiBookmark(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-bookmark"" viewBox=""0 0 16 16"">
                <path d=""M2 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v13.5a.5.5 0 0 1-.777.416L8 13.101l-5.223 2.815A.5.5 0 0 1 2 15.5zm2-1a1 1 0 0 0-1 1v12.566l4.723-2.482a.5.5 0 0 1 .554 0L13 14.566V2a1 1 0 0 0-1-1z""/>
            </svg>
            "
            );

        public static MarkupString BiBookmarkCheckFill(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-bookmark-check-fill"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M2 15.5V2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v13.5a.5.5 0 0 1-.74.439L8 13.069l-5.26 2.87A.5.5 0 0 1 2 15.5m8.854-9.646a.5.5 0 0 0-.708-.708L7.5 7.793 6.354 6.646a.5.5 0 1 0-.708.708l1.5 1.5a.5.5 0 0 0 .708 0z""/>
            </svg>
            "
            );

        public static MarkupString BiBoundingBox(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-bounding-box"" viewBox=""0 0 16 16"">
                <path d=""M5 2V0H0v5h2v6H0v5h5v-2h6v2h5v-5h-2V5h2V0h-5v2zm6 1v2h2v6h-2v2H5v-2H3V5h2V3zm1-2h3v3h-3zm3 11v3h-3v-3zM4 15H1v-3h3zM1 4V1h3v3z"" />
            </svg>
            "
            );

        public static MarkupString BiDiagram2(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-diagram-2"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M6 3.5A1.5 1.5 0 0 1 7.5 2h1A1.5 1.5 0 0 1 10 3.5v1A1.5 1.5 0 0 1 8.5 6v1H11a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0v-1A.5.5 0 0 1 5 7h2.5V6A1.5 1.5 0 0 1 6 4.5zM8.5 5a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5zM3 11.5A1.5 1.5 0 0 1 4.5 10h1A1.5 1.5 0 0 1 7 11.5v1A1.5 1.5 0 0 1 5.5 14h-1A1.5 1.5 0 0 1 3 12.5zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm4.5.5a1.5 1.5 0 0 1 1.5-1.5h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1A1.5 1.5 0 0 1 9 12.5zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5z""/>
            </svg>
            "
            );

        public static MarkupString BiDiagram3(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-diagram-3"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M6 3.5A1.5 1.5 0 0 1 7.5 2h1A1.5 1.5 0 0 1 10 3.5v1A1.5 1.5 0 0 1 8.5 6v1H14a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0v-1A.5.5 0 0 1 2 7h5.5V6A1.5 1.5 0 0 1 6 4.5zM8.5 5a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5zM0 11.5A1.5 1.5 0 0 1 1.5 10h1A1.5 1.5 0 0 1 4 11.5v1A1.5 1.5 0 0 1 2.5 14h-1A1.5 1.5 0 0 1 0 12.5zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm4.5.5A1.5 1.5 0 0 1 7.5 10h1a1.5 1.5 0 0 1 1.5 1.5v1A1.5 1.5 0 0 1 8.5 14h-1A1.5 1.5 0 0 1 6 12.5zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm4.5.5a1.5 1.5 0 0 1 1.5-1.5h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1a1.5 1.5 0 0 1-1.5-1.5zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5z""/>
            </svg>
            "
            );

        public static MarkupString BiFiletypeCs(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-filetype-cs"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M14 4.5V14a2 2 0 0 1-2 2H8v-1h4a1 1 0 0 0 1-1V4.5h-2A1.5 1.5 0 0 1 9.5 3V1H4a1 1 0 0 0-1 1v9H2V2a2 2 0 0 1 2-2h5.5zM3.629 15.29a1.2 1.2 0 0 1-.112-.449h.765a.58.58 0 0 0 .255.384q.105.073.249.114t.32.041q.245 0 .412-.07a.56.56 0 0 0 .255-.193.5.5 0 0 0 .085-.29.39.39 0 0 0-.152-.326q-.153-.12-.463-.193l-.618-.143a1.7 1.7 0 0 1-.54-.214 1 1 0 0 1-.35-.367 1.1 1.1 0 0 1-.124-.524q0-.366.19-.639.191-.272.528-.422t.776-.149q.458 0 .78.152.324.153.5.41.18.255.2.566h-.75a.56.56 0 0 0-.12-.258.6.6 0 0 0-.246-.181.9.9 0 0 0-.37-.068q-.324 0-.512.152a.47.47 0 0 0-.185.384q0 .18.144.3a1 1 0 0 0 .404.175l.621.143q.325.075.566.211t.375.358.134.56q0 .37-.187.656a1.2 1.2 0 0 1-.54.439q-.351.158-.858.158a2.2 2.2 0 0 1-.665-.09 1.4 1.4 0 0 1-.477-.252 1.1 1.1 0 0 1-.29-.375m-2.72-2.23a1.7 1.7 0 0 0-.103.633v.495q0 .369.102.627a.83.83 0 0 0 .299.392.85.85 0 0 0 .478.132.86.86 0 0 0 .4-.088.7.7 0 0 0 .273-.249.8.8 0 0 0 .118-.363h.764v.076a1.27 1.27 0 0 1-.225.674q-.205.29-.551.454a1.8 1.8 0 0 1-.785.164q-.54 0-.914-.217a1.4 1.4 0 0 1-.572-.626Q0 14.756 0 14.188v-.498q0-.569.196-.979a1.44 1.44 0 0 1 .572-.633q.378-.222.91-.222.33 0 .607.097.281.093.49.272a1.32 1.32 0 0 1 .465.964v.073h-.764a.85.85 0 0 0-.12-.38.7.7 0 0 0-.273-.261.8.8 0 0 0-.398-.097.8.8 0 0 0-.475.138.87.87 0 0 0-.302.398Z""/>
            </svg>
            "
            );

        public static MarkupString BiFlag(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-flag"" viewBox=""0 0 16 16"">
                <path d=""M14.778.085A.5.5 0 0 1 15 .5V8a.5.5 0 0 1-.314.464L14.5 8l.186.464-.003.001-.006.003-.023.009a12 12 0 0 1-.397.15c-.264.095-.631.223-1.047.35-.816.252-1.879.523-2.71.523-.847 0-1.548-.28-2.158-.525l-.028-.01C7.68 8.71 7.14 8.5 6.5 8.5c-.7 0-1.638.23-2.437.477A20 20 0 0 0 3 9.342V15.5a.5.5 0 0 1-1 0V.5a.5.5 0 0 1 1 0v.282c.226-.079.496-.17.79-.26C4.606.272 5.67 0 6.5 0c.84 0 1.524.277 2.121.519l.043.018C9.286.788 9.828 1 10.5 1c.7 0 1.638-.23 2.437-.477a20 20 0 0 0 1.349-.476l.019-.007.004-.002h.001M14 1.221c-.22.078-.48.167-.766.255-.81.252-1.872.523-2.734.523-.886 0-1.592-.286-2.203-.534l-.008-.003C7.662 1.21 7.139 1 6.5 1c-.669 0-1.606.229-2.415.478A21 21 0 0 0 3 1.845v6.433c.22-.078.48-.167.766-.255C4.576 7.77 5.638 7.5 6.5 7.5c.847 0 1.548.28 2.158.525l.028.01C9.32 8.29 9.86 8.5 10.5 8.5c.668 0 1.606-.229 2.415-.478A21 21 0 0 0 14 7.655V1.222z""/>
            </svg>
            "
            );

        public static MarkupString BiFlagFill(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-flag-fill"" viewBox=""0 0 16 16"">
                <path d=""M14.778.085A.5.5 0 0 1 15 .5V8a.5.5 0 0 1-.314.464L14.5 8l.186.464-.003.001-.006.003-.023.009a12 12 0 0 1-.397.15c-.264.095-.631.223-1.047.35-.816.252-1.879.523-2.71.523-.847 0-1.548-.28-2.158-.525l-.028-.01C7.68 8.71 7.14 8.5 6.5 8.5c-.7 0-1.638.23-2.437.477A20 20 0 0 0 3 9.342V15.5a.5.5 0 0 1-1 0V.5a.5.5 0 0 1 1 0v.282c.226-.079.496-.17.79-.26C4.606.272 5.67 0 6.5 0c.84 0 1.524.277 2.121.519l.043.018C9.286.788 9.828 1 10.5 1c.7 0 1.638-.23 2.437-.477a20 20 0 0 0 1.349-.476l.019-.007.004-.002h.001""/>
            </svg>
            "
            );

        public static MarkupString BiFloppy(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-floppy"" viewBox=""0 0 16 16"">
                <path d=""M11 2H9v3h2z""/>
                <path d=""M1.5 0h11.586a1.5 1.5 0 0 1 1.06.44l1.415 1.414A1.5 1.5 0 0 1 16 2.914V14.5a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 14.5v-13A1.5 1.5 0 0 1 1.5 0M1 1.5v13a.5.5 0 0 0 .5.5H2v-4.5A1.5 1.5 0 0 1 3.5 9h9a1.5 1.5 0 0 1 1.5 1.5V15h.5a.5.5 0 0 0 .5-.5V2.914a.5.5 0 0 0-.146-.353l-1.415-1.415A.5.5 0 0 0 13.086 1H13v4.5A1.5 1.5 0 0 1 11.5 7h-7A1.5 1.5 0 0 1 3 5.5V1H1.5a.5.5 0 0 0-.5.5m3 4a.5.5 0 0 0 .5.5h7a.5.5 0 0 0 .5-.5V1H4zM3 15h10v-4.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5z""/>
            </svg>
            "
            );

        public static MarkupString BiFolder2Open(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-folder2-open"" viewBox=""0 0 16 16"">
                <path d=""M1 3.5A1.5 1.5 0 0 1 2.5 2h2.764c.958 0 1.76.56 2.311 1.184C7.985 3.648 8.48 4 9 4h4.5A1.5 1.5 0 0 1 15 5.5v.64c.57.265.94.876.856 1.546l-.64 5.124A2.5 2.5 0 0 1 12.733 15H3.266a2.5 2.5 0 0 1-2.481-2.19l-.64-5.124A1.5 1.5 0 0 1 1 6.14zM2 6h12v-.5a.5.5 0 0 0-.5-.5H9c-.964 0-1.71-.629-2.174-1.154C6.374 3.334 5.82 3 5.264 3H2.5a.5.5 0 0 0-.5.5zm-.367 1a.5.5 0 0 0-.496.562l.64 5.124A1.5 1.5 0 0 0 3.266 14h9.468a1.5 1.5 0 0 0 1.489-1.314l.64-5.124A.5.5 0 0 0 14.367 7z""/>
            </svg>
            "
            );

        public static MarkupString BiFront(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-front"" viewBox=""0 0 16 16"">
                <path d=""M0 2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v2h2a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2v-2H2a2 2 0 0 1-2-2zm5 10v2a1 1 0 0 0 1 1h8a1 1 0 0 0 1-1V6a1 1 0 0 0-1-1h-2v5a2 2 0 0 1-2 2z""/>
            </svg>
            "
            );

        public static MarkupString BiHouse(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-house"" viewBox=""0 0 16 16"">
                <path d=""M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293zM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5z""/>
            </svg>
            "
            );

        public static MarkupString BiJournalCode(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-journal-code"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M8.646 5.646a.5.5 0 0 1 .708 0l2 2a.5.5 0 0 1 0 .708l-2 2a.5.5 0 0 1-.708-.708L10.293 8 8.646 6.354a.5.5 0 0 1 0-.708m-1.292 0a.5.5 0 0 0-.708 0l-2 2a.5.5 0 0 0 0 .708l2 2a.5.5 0 0 0 .708-.708L5.707 8l1.647-1.646a.5.5 0 0 0 0-.708"" />
                <path d=""M3 0h10a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2v-1h1v1a1 1 0 0 0 1 1h10a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H3a1 1 0 0 0-1 1v1H1V2a2 2 0 0 1 2-2"" />
                <path d=""M1 5v-.5a.5.5 0 0 1 1 0V5h.5a.5.5 0 0 1 0 1h-2a.5.5 0 0 1 0-1zm0 3v-.5a.5.5 0 0 1 1 0V8h.5a.5.5 0 0 1 0 1h-2a.5.5 0 0 1 0-1zm0 3v-.5a.5.5 0 0 1 1 0v.5h.5a.5.5 0 0 1 0 1h-2a.5.5 0 0 1 0-1z"" />
            </svg>
            "
            );

        public static MarkupString BiNodeMinus(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-node-minus"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M11 4a4 4 0 1 0 0 8 4 4 0 0 0 0-8M6.025 7.5a5 5 0 1 1 0 1H4A1.5 1.5 0 0 1 2.5 10h-1A1.5 1.5 0 0 1 0 8.5v-1A1.5 1.5 0 0 1 1.5 6h1A1.5 1.5 0 0 1 4 7.5zM1.5 7a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zM8 8a.5.5 0 0 1 .5-.5h5a.5.5 0 0 1 0 1h-5A.5.5 0 0 1 8 8""/>
            </svg>
            "
            );

        public static MarkupString BiNodePlus(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-node-plus"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M11 4a4 4 0 1 0 0 8 4 4 0 0 0 0-8M6.025 7.5a5 5 0 1 1 0 1H4A1.5 1.5 0 0 1 2.5 10h-1A1.5 1.5 0 0 1 0 8.5v-1A1.5 1.5 0 0 1 1.5 6h1A1.5 1.5 0 0 1 4 7.5zM11 5a.5.5 0 0 1 .5.5v2h2a.5.5 0 0 1 0 1h-2v2a.5.5 0 0 1-1 0v-2h-2a.5.5 0 0 1 0-1h2v-2A.5.5 0 0 1 11 5M1.5 7a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5z""/>
            </svg>
            "
            );

        public static MarkupString BiPencil(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-pencil"" viewBox=""0 0 16 16"">
                <path d=""M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325""/>
            </svg>
            "
            );

        public static MarkupString BiPlus(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-plus"" viewBox=""0 0 16 16"">
                <path d=""M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4""/>
            </svg>
            "
            );

        public static MarkupString BiPlusCircle(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-plus-circle"" viewBox=""0 0 16 16"">
                <path d=""M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16""/>
                <path d=""M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4""/>
            </svg>
            "
            );

        public static MarkupString BiPlusSquare(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-plus-square"" viewBox=""0 0 16 16"">
                <path d=""M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2z""/>
                <path d=""M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4""/>
            </svg>
            "
            );

        public static MarkupString BiRegex(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-regex"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M3.05 3.05a7 7 0 0 0 0 9.9.5.5 0 0 1-.707.707 8 8 0 0 1 0-11.314.5.5 0 1 1 .707.707m9.9-.707a.5.5 0 0 1 .707 0 8 8 0 0 1 0 11.314.5.5 0 0 1-.707-.707 7 7 0 0 0 0-9.9.5.5 0 0 1 0-.707M6 11a1 1 0 1 1-2 0 1 1 0 0 1 2 0m5-6.5a.5.5 0 0 0-1 0v2.117L8.257 5.57a.5.5 0 0 0-.514.858L9.528 7.5 7.743 8.571a.5.5 0 1 0 .514.858L10 8.383V10.5a.5.5 0 1 0 1 0V8.383l1.743 1.046a.5.5 0 0 0 .514-.858L11.472 7.5l1.785-1.071a.5.5 0 1 0-.514-.858L11 6.617z"" />
            </svg>
            "
            );

        public static MarkupString BiRepeat(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-repeat"" viewBox=""0 0 16 16"">
                <path d=""M11 5.466V4H5a4 4 0 0 0-3.584 5.777.5.5 0 1 1-.896.446A5 5 0 0 1 5 3h6V1.534a.25.25 0 0 1 .41-.192l2.36 1.966c.12.1.12.284 0 .384l-2.36 1.966a.25.25 0 0 1-.41-.192m3.81.086a.5.5 0 0 1 .67.225A5 5 0 0 1 11 13H5v1.466a.25.25 0 0 1-.41.192l-2.36-1.966a.25.25 0 0 1 0-.384l2.36-1.966a.25.25 0 0 1 .41.192V12h6a4 4 0 0 0 3.585-5.777.5.5 0 0 1 .225-.67Z""/>
            </svg>
            "
            );

        public static MarkupString BiSignpostSplit(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-signpost-split"" viewBox=""0 0 16 16"">
                <path d=""M7 7V1.414a1 1 0 0 1 2 0V2h5a1 1 0 0 1 .8.4l.975 1.3a.5.5 0 0 1 0 .6L14.8 5.6a1 1 0 0 1-.8.4H9v10H7v-5H2a1 1 0 0 1-.8-.4L.225 9.3a.5.5 0 0 1 0-.6L1.2 7.4A1 1 0 0 1 2 7zm1 3V8H2l-.75 1L2 10zm0-5h6l.75-1L14 3H8z""/>
            </svg>
            "
            );

        public static MarkupString BiStickies(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-stickies"" viewBox=""0 0 16 16"">
                <path d=""M1.5 0A1.5 1.5 0 0 0 0 1.5V13a1 1 0 0 0 1 1V1.5a.5.5 0 0 1 .5-.5H14a1 1 0 0 0-1-1z""/>
                <path d=""M3.5 2A1.5 1.5 0 0 0 2 3.5v11A1.5 1.5 0 0 0 3.5 16h6.086a1.5 1.5 0 0 0 1.06-.44l4.915-4.914A1.5 1.5 0 0 0 16 9.586V3.5A1.5 1.5 0 0 0 14.5 2zM3 3.5a.5.5 0 0 1 .5-.5h11a.5.5 0 0 1 .5.5V9h-4.5A1.5 1.5 0 0 0 9 10.5V15H3.5a.5.5 0 0 1-.5-.5zm7 11.293V10.5a.5.5 0 0 1 .5-.5h4.293z""/>
            </svg>
            "
            );

        public static MarkupString BiTerminal(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-terminal"" viewBox=""0 0 16 16"">
                <path d=""M6 9a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3A.5.5 0 0 1 6 9M3.854 4.146a.5.5 0 1 0-.708.708L4.793 6.5 3.146 8.146a.5.5 0 1 0 .708.708l2-2a.5.5 0 0 0 0-.708z""/>
                <path d=""M2 1a2 2 0 0 0-2 2v10a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2zm12 1a1 1 0 0 1 1 1v10a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V3a1 1 0 0 1 1-1z""/>
            </svg>
            "
            );

        public static MarkupString BiTrash(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-trash"" viewBox=""0 0 16 16"">
                <path d=""M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5m3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0z""/>
                <path d=""M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4zM2.5 3h11V2h-11z""/>
            </svg>
            "
            );

        public static MarkupString BiWrenchAdjustable(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-wrench-adjustable"" viewBox=""0 0 16 16"">
                <path d=""M16 4.5a4.5 4.5 0 0 1-1.703 3.526L13 5l2.959-1.11q.04.3.041.61""/>
                <path d=""M11.5 9c.653 0 1.273-.139 1.833-.39L12 5.5 11 3l3.826-1.53A4.5 4.5 0 0 0 7.29 6.092l-6.116 5.096a2.583 2.583 0 1 0 3.638 3.638L9.908 8.71A4.5 4.5 0 0 0 11.5 9m-1.292-4.361-.596.893.809-.27a.25.25 0 0 1 .287.377l-.596.893.809-.27.158.475-1.5.5a.25.25 0 0 1-.287-.376l.596-.893-.809.27a.25.25 0 0 1-.287-.377l.596-.893-.809.27-.158-.475 1.5-.5a.25.25 0 0 1 .287.376M3 14a1 1 0 1 1 0-2 1 1 0 0 1 0 2""/>
            </svg>
            "
            );

        public static MarkupString BiZoomIn(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-zoom-in"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11M13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0""/>
                <path d=""M10.344 11.742q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1 6.5 6.5 0 0 1-1.398 1.4z""/>
                <path fill-rule=""evenodd"" d=""M6.5 3a.5.5 0 0 1 .5.5V6h2.5a.5.5 0 0 1 0 1H7v2.5a.5.5 0 0 1-1 0V7H3.5a.5.5 0 0 1 0-1H6V3.5a.5.5 0 0 1 .5-.5""/>
            </svg>
            "
            );

        public static MarkupString BiZoomOut(decimal size = DEFAULT_SIZE) => new(
            @$"
            <svg xmlns=""http://www.w3.org/2000/svg"" width=""{size:0.###}"" height=""{size:0.###}"" fill=""currentColor"" class=""bi bi-zoom-out"" viewBox=""0 0 16 16"">
                <path fill-rule=""evenodd"" d=""M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11M13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0""/>
                <path d=""M10.344 11.742q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1 6.5 6.5 0 0 1-1.398 1.4z""/>
                <path fill-rule=""evenodd"" d=""M3 6.5a.5.5 0 0 1 .5-.5h6a.5.5 0 0 1 0 1h-6a.5.5 0 0 1-.5-.5""/>
            </svg>
            "
            );
    }
}
