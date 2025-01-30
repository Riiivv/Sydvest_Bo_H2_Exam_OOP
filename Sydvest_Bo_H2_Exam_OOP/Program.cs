using Sydvest_Bo_H2_Exam_OOP.models;
using Microsoft.Data.SqlClient;
using Sydvest_Bo_H2_Exam_OOP;
var menu = new MenuHandler();
menu.StartMenu((menuText) =>
{
    Console.WriteLine(menuText);
    return Console.ReadLine();
},
(message) => Console.WriteLine(message));