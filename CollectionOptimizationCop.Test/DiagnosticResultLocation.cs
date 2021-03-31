namespace CollectionOptimizationCop.Test
{
    using System;

    using Microsoft.CodeAnalysis;

    /// <summary>
    ///     Location where the diagnostic appears, as determined by path, line number, and column number.
    /// </summary>
    public struct DiagnosticResultLocation
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DiagnosticResultLocation"/> struct.
        /// </summary>
        /// <param name="path">
        ///     The path to the file where the diagnostic occurred.
        /// </param>
        /// <param name="line">
        ///     The line in the file where the diagnostic occurred.
        /// </param>
        /// <param name="column">
        ///     The column in the file where the diagnostic occured.
        /// </param>
        public DiagnosticResultLocation(string path, int line, int column)
        {
            if (line < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(line), "line must be >= -1");
            }

            if (column < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(column), "column must be >= -1");
            }

            this.Path = path;
            this.Line = line;
            this.Column = column;
        }

        /// <summary>
        ///     Gets the path to file where the diagnostic occurred.
        /// </summary>
        public string Path { get; }

        /// <summary>
        ///     Gets the line in the file where the diagnostic occurred.
        /// </summary>
        public int Line { get; }

        /// <summary>
        ///     Gets the column in the file where the diagnostic occurred.
        /// </summary>
        public int Column { get; }
    }

    /// <summary>
    ///     Struct that stores information about a Diagnostic appearing in a source.
    /// </summary>
    public struct DiagnosticResult
    {
        private DiagnosticResultLocation[] locations;

        /// <summary>
        ///     Gets or sets the locations where the diagnostic occurred.
        /// </summary>
        public DiagnosticResultLocation[] Locations
        {
            get
            {
                if (this.locations == null)
                {
                    this.locations = new DiagnosticResultLocation[] { };
                }

                return this.locations;
            }

            set => this.locations = value;
        }

        /// <summary>
        ///     Gets or sets the severity of the diagnostic.
        /// </summary>
        public DiagnosticSeverity Severity { get; set; }

        /// <summary>
        ///     Gets or sets the ID of the diagnostic.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the message of the diagnostic.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets the path to first file where the diagnostic occurred.
        /// </summary>
        public string Path => this.Locations.Length > 0 ? this.Locations[0].Path : string.Empty;

        /// <summary>
        ///     Gets the line in the first file where the diagnostic occurred.
        /// </summary>
        public int Line => this.Locations.Length > 0 ? this.Locations[0].Line : -1;

        /// <summary>
        ///     Gets the column in the first file where the diagnostic occurred.
        /// </summary>
        public int Column => this.Locations.Length > 0 ? this.Locations[0].Column : -1;
    }
}
