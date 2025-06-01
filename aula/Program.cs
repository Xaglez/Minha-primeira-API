class Aula
{
    static void Main(string[] args)
    {
        string s = "{()}";
        bool result = IsValid(s);
        Console.WriteLine(result ? "Valid" : "Invalid");
    }

    public static bool IsValid(string s) {
        Dictionary<char, char> map = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' }
        };
        Stack<char> stack = new Stack<char>();
        foreach (char c in s) {
            if (map.ContainsKey(c)) {
                stack.Push(map[c]);
            } else if (stack.Count == 0 || stack.Pop() != c) {
                return false;
            }
        }
        if (stack.Count > 0) {
            return false;
        }

        return true;
    }
}

