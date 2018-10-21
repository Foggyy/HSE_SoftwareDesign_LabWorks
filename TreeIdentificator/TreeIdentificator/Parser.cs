using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TreeIdentificator
{
    public class Parser
    {
       static Regex VarPattern = new Regex(@"^(?<data>int|float|bool|string|char)\s(?<name>\w+)$");
       static Regex ConstPattern = new Regex(@"^const\s(?<data>int|float|bool|string|char)\s(?<name>\w+)\s(?<value>\d+.\d+)|(?<value>\d+)$");
       static Regex ClassPattern = new Regex(@"^class\s(?<name>\w+)$");
       static Regex MethodPattern = new Regex(@"^(?<data>\w+)\s(?<name>\w+)\((?<params>.*)\)$");
       static Regex MethodParamsPattern = new Regex(@"((?<paramType>\w+)\s)?(?<type>\w+)\s\w+");

        static public BinarTree Parse(string[] values)
       {
           bool ok = true;
            BinarTree Tree = null;

            foreach (string value in values)
            {
                if (ClassPattern.IsMatch(value))                    //Классы
                {
                    Match match = ClassPattern.Match(value);
                    string name = match.Groups["name"].Value;       //берем из строки значение имени
                    Classes Element = new Classes(name);            //создаем объект класса с взятыми значениями
                    BinarTree Branch = new BinarTree(Element);      //создаем объект дерева
                    if (ok)                                         //создание первого объекта дерева
                    {
                        Tree = Branch;
                        ok = false;
                    }
                    else
                    Tree.AddTree(Branch);                           //добавляем новый элемент дерева
                }
                else if (VarPattern.IsMatch(value))                 //Переменные
                {
                    Match match = VarPattern.Match(value);
                    string name = match.Groups["name"].Value;
                    string data = match.Groups["data"].Value;       //тип данных
                    Var Element = new Var(name,data);
                    BinarTree Branch = new BinarTree(Element);
                    if (ok)
                    {
                        Tree = Branch;
                        ok = false;
                    }
                    else
                        Tree.AddTree(Branch);
                }
                else if (ConstPattern.IsMatch(value))
                {
                    Match match = ConstPattern.Match(value);
                    string name = match.Groups["name"].Value;
                    string data = match.Groups["data"].Value;
                    object Var = null;

                    if (data == "int")
                    {
                        Var = int.Parse(match.Groups["value"].Value);
                    }
                    if (data == "float")
                    {
                        Var = float.Parse(match.Groups["value"].Value);
                    }
                    if (data == "string")
                    {
                        Var = match.Groups["value"].Value;
                    }
                    if (data == "bool")
                    {
                        if (match.Groups["value"].Value == "true")
                        {
                            Var = true;
                        }
                        else
                        {
                            Var = false;
                        }
                    }

                    if (data == "char")
                    {
                        Var = char.Parse(match.Groups["value"].Value);
                    }
                    Const Element = new Const(name,data,Var);
                    BinarTree Branch = new BinarTree(Element);
                    if (ok)
                    {
                        Tree = Branch;
                        ok = false;
                    }
                    else
                        Tree.AddTree(Branch);
                }
                else if (MethodPattern.IsMatch(value))                          //Методы
                {
                   
                    Match match = MethodPattern.Match(value);
                    string name = match.Groups["name"].Value;
                    string data = match.Groups["data"].Value;
                    if (match.Groups["params"].Value == String.Empty)
                    {
                        Method MyMethod;
                        MyMethod = new Method(name, data, null);
                        BinarTree Branch1 = new BinarTree(MyMethod);
                        Tree.AddTree(Branch1);
                        continue;
                    }
                    string[] parameters = match.Groups["params"].Value.Split(',');  //строковый массив параметров
                    List<MethodParams> ParamList = new List<MethodParams>();        //объявление списка параметров

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Match parameterMatch = MethodParamsPattern.Match(parameters[i]);
                        string parameterType = parameterMatch.Groups["type"].Value;

                        ParametrType parameterUsageType = ParametrType.param_val;   //стандартное значение типа параметра

                        if (parameterMatch.Groups["paramType"].Value != String.Empty)
                            switch (parameterMatch.Groups["paramType"].Value)
                            {
                                case "ref":
                                    parameterUsageType = ParametrType.param_ref;    //ссылочный тип ref у параметра
                                    break;
                                case "out":
                                    parameterUsageType = ParametrType.param_out;    //ссылочный тип out у параметра
                                    break;
                            }
                        MethodParams Element = new MethodParams(parameterType,parameterUsageType);
                        ParamList.Add(Element);
                    }
                    Method method = new Method(name,data,ParamList);                //объект класса Метод
                    BinarTree Branch = new BinarTree(method);
                    Tree.AddTree(Branch);                                           //добавляем объект в древо
                }
                else
                {
                    Console.WriteLine("\nДанный элемент не был добавлен, проверьте ввод:");
                    Console.WriteLine(value);                 
                }
            }

            return Tree;
        }
    }
}
