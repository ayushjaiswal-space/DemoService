using System.Reflection;
using NetArchTest.Rules;

namespace {{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}.ArchitectureTests;

public static class Extensions
{
    public static string GetFailingTypeNames(this TestResult result)
    {
        return result.FailingTypeNames == null ? string.Empty : string.Join(", ", result.FailingTypeNames);
    }

    internal static bool TypeShouldNotBeDecoratedWith(Type typeToBeChecked, string sourceNamespace)
    {
        IEnumerable<Type> types = Types.InNamespace(sourceNamespace).GetTypes();
        foreach (Type type in types)
        {
            if (TypesExcludedFromCodeCoverage.IsExcluded(type))
            {
                continue;
            }

            Attribute typeAttribute = type.GetCustomAttribute(typeToBeChecked);
            if (typeAttribute != null)
            {
                Console.WriteLine(type.FullName);
                return false;
            }

            if (!MembersShouldNotBeDecoratedWith(typeToBeChecked, type))
            {
                return false;
            }
        }

        return true;
    }

    private static bool MembersShouldNotBeDecoratedWith(Type typeToBeChecked, Type type)
    {
        MemberInfo[] members = type.GetMembers();
        foreach (MemberInfo member in members)
        {
            Attribute attribute = member.GetCustomAttribute(typeToBeChecked);
            if (attribute != null)
            {
                Console.WriteLine(type.FullName);
                return false;
            }
        }

        return true;
    }
}