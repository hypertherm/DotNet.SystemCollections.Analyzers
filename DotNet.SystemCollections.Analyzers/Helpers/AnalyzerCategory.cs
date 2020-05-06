namespace DotNet.SystemCollections.Analyzers.Helpers
{
    /// <summary>
    ///     This is the listing of analyzer categories.
    /// </summary>
    internal static class AnalyzerCategory
    {
        /// <summary>
        ///     This category is for those analyzers operating on arrays.
        /// </summary>
        internal const string Arrays = "Arrays";

        /// <summary>
        ///     This category is for those analyzers operating on (Generic) collections (i.e. those from System.Collections.Generic).
        /// </summary>
        internal const string Collections = "Collections";

        /// <summary>
        ///     This category is focused for those analyzers operating on "Old-Style" collections (i.e. those from System.Collections).
        /// </summary>
        internal const string OldStyleCollections = "\"Old-Style\" Collections";
    }
}
