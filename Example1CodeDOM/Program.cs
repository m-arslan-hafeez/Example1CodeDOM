using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

class Program{
    static void Main(string[] args)
    {
        // Create a CodeCompileUnit
        CodeCompileUnit compileUnitObj = new CodeCompileUnit();

        // Create a namespace
        CodeNamespace namespaceObj = new CodeNamespace("NamespaceCodeDOM");

        // Add a reference to the System namespace
        CodeNamespaceImport systemNamespace = new CodeNamespaceImport("System");
        namespaceObj.Imports.Add(systemNamespace);

        // Create a class
        CodeTypeDeclaration classDeclarationObj = new CodeTypeDeclaration("ClassCodeDOM");

        // Creating methods
        CodeMemberMethod methodMessageObj = new CodeMemberMethod();
        methodMessageObj.Name = "methodMessage";
        methodMessageObj.Attributes = MemberAttributes.Public | MemberAttributes.Static;
        methodMessageObj.ReturnType = new CodeTypeReference(typeof(void));

        // Add a statement to the method with your specific message
        methodMessageObj.Statements.Add(new CodeSnippetStatement("System.Console.WriteLine(\"Hello this is my first CodeDOM message\");"));

        // Add the method to the class
        classDeclarationObj.Members.Add(methodMessageObj);

        // Create a new method methodSum
        CodeMemberMethod methodSumObj = new CodeMemberMethod();
        methodSumObj.Name = "methodSum";
        methodSumObj.Attributes = MemberAttributes.Public | MemberAttributes.Static;
        methodSumObj.ReturnType = new CodeTypeReference(typeof(int)); // Change the return type to int

        // Add parameters to methodSum
        methodSumObj.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "a"));
        methodSumObj.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "b"));

        // Add a statement to calculate and return the sum
        methodSumObj.Statements.Add(new CodeSnippetStatement("return a + b;"));

        // Add a statement to calculate and return the sum
        methodSumObj.Statements.Add(new CodeSnippetStatement("return a + b;"));

        // Add the methodSum to the class
        classDeclarationObj.Members.Add(methodSumObj);

        // Add the class to the namespace
        namespaceObj.Types.Add(classDeclarationObj);

        // Add the namespace to the compile unit
        compileUnitObj.Namespaces.Add(namespaceObj);

        // Use the CSharpCodeProvider to compile and execute the code
        CSharpCodeProvider csProviderObj = new CSharpCodeProvider();
        CompilerParameters parametersObj = new CompilerParameters();
        parametersObj.GenerateInMemory = true;

        CompilerResults results = csProviderObj.CompileAssemblyFromDom(parametersObj, compileUnitObj);

        if (results.Errors.HasErrors)
        {
            foreach (CompilerError error in results.Errors)
            {
                Console.WriteLine(error.ErrorText);
            }
        }
        else
        {
            // Execute the methodMessage
            Type type = results.CompiledAssembly.GetType("NamespaceCodeDOM.ClassCodeDOM");
            var methodMessageInfo = type.GetMethod("methodMessage");
            methodMessageInfo.Invoke(null, null);

            // Execute the methodSum
            var methodSumInfo = type.GetMethod("methodSum");
            // Call methodSum and print the result
            int var1, var2;
            Console.WriteLine("Enter two values : ");
            var1 = Convert.ToInt32(Console.ReadLine());
            var2 = Convert.ToInt32(Console.ReadLine());
            int result = (int)methodSumInfo.Invoke(null, new object[] { var1, var2 });
            Console.WriteLine($"Sum of {var1} and {var2} is : {result}");
        }

        Console.ReadKey();
    }
}
