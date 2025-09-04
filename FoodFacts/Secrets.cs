namespace FoodFacts;

// This is the public template file that is safe to commit to GitHub.
// The 'partial' keyword allows its definition to be split across multiple files.
internal static partial class Secrets
{
    // The secret key is defined here, but its value is assigned in the local file.
    internal static readonly string DummyApiKey;
}