using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using Manager;
using Model;

namespace ClassReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion
            //List<ColumnData> columnDatas = new ColumnManager().GetColumns();
            //string aa = string.Empty;
            //Dictionary<string, string> typeDict = new Dictionary<string, string>
            //{
            //    {"CHAR", "string"},
            //    {"NUMBER","double" },
            //    {"VARCHAR2","string" },
            //    {"CLOB","string" }
            //};
            //StringBuilder strBuilder = new StringBuilder();
            //strBuilder.AppendLine("namespace Model");
            //strBuilder.AppendLine("{");
            //strBuilder.AppendLine("public class TTRD_CDS");
            //strBuilder.AppendLine("{");
            //foreach (var data in columnDatas)
            //{
            //    strBuilder.AppendFormat("public {0} {1} ", typeDict[data.ColumnType], data.ColumnName);
            //    strBuilder.AppendLine("{ get; set; }");
            //}
            //strBuilder.AppendLine("}");
            //strBuilder.AppendLine("}");
            //Console.WriteLine(columnDatas.Count);
            //aa = strBuilder.ToString();
            //foreach (var item in columnDatas)
            //{
            //    Console.WriteLine(item.ColumnName + "  " + item.ColumnType);
            //} 
            #endregion

            BuildClass();
            Console.ReadKey();
        }

        public static void BuildClass()
        {
            // 代码编译器单元
            CodeCompileUnit unit = new CodeCompileUnit();

            // 设置命名空间
            CodeNamespace myNamespace = new CodeNamespace("Model");

            // 引用命名空间
            myNamespace.Imports.Add(new CodeNamespaceImport("System"));

            // 代码体
            CodeTypeDeclaration myClass = new CodeTypeDeclaration("TTRD_CDS");

            // 指定为类
            myClass.IsClass = true;

            // 设置类的访问类型
            myClass.TypeAttributes = System.Reflection.TypeAttributes.Public;

            // 把类放到myNamespace命名空间下
            myNamespace.Types.Add(myClass);

            // 把myNamespace加入到unit编译器单元的命名空间集合中
            unit.Namespaces.Add(myNamespace);

            List<ColumnData> columnDatas = new ColumnManager().GetColumns();
            foreach (var columnData in columnDatas)
            {
                // 获取类型
                CodeTypeReference codeType = GetPropertyType(columnData.ColumnType);

                // 字段名称
                string fieldName = $"_{columnData.ColumnName.ToLower()}";

                // 添加字段
                CodeMemberField field = new CodeMemberField(codeType, fieldName);

                // 设置字段的访问类型
                field.Attributes = MemberAttributes.Private;

                // 字段添加到类
                myClass.Members.Add(field);


                // 添加属性
                CodeMemberProperty property = new CodeMemberProperty();

                // 设置属性的访问类型
                property.Attributes = MemberAttributes.Public;

                // 设置属性名称
                property.Name = columnData.ColumnName;

                property.Type = codeType;

                // 设置get;set
                property.HasGet = true;
                property.HasSet = true;

                //property.GetStatements.Add(new CodeStatement());
                //property.SetStatements.Add(new CodeStatement());

                property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));
                property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodePropertySetValueReferenceExpression()));

                // 将属性添加到类中
                myClass.Members.Add(property);
            }

            // 生成C#脚本
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            CodeGeneratorOptions options = new CodeGeneratorOptions();

            // 代码风格：大括号{}
            options.BracingStyle = "C";

            // 是否字段属性之间添加空白行
            options.BlankLinesBetweenMembers = true;

            // 输出文件路径
            string outputFile = @"C:\Users\Administrator\source\repos\ClassReflection3\Model\" + "TTRD_CDS" + ".cs";

            // 保存流
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                // 为指定的代码文档对象模型(CodeDOM) 编译单元生成代码并将其发送到指定的文本编写器，使用指定的选项
                // 将自定义代码编译器(代码内容)、和代码格式写入到sw中
                provider.GenerateCodeFromCompileUnit(unit, sw, options);
            }
        }

        public static CodeTypeReference GetPropertyType(string columnType)
        {
            CodeTypeReference codeType = new CodeTypeReference();
            switch (columnType)
            {
                case "NUMBER":
                    codeType = new CodeTypeReference(typeof(System.Double));
                    break;
                case "CHAR":
                case "VARCHAR2":
                case "CLOB":
                    codeType = new CodeTypeReference(typeof(System.String));
                    break;
                default:
                    break;
            }

            return codeType;
        }
    }
}
