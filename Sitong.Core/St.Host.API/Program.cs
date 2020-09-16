using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using St.Application.Infrastruct.Identity;
using St.Common.Helper;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace St.Host.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * �꿨�Ϳ�ר����������,���������
             */
            Console.WriteLine("�꿨�Ϳ� �����ۿ� �׿��ۿ� ķ\r\n" +
                "�꿨�Ϳ� ����ѽ�� �������� ��\r\n" +
                "��ķ��ķ ������ ��ѽӴ\r\n" +
                "�꿨�Ϳ� �����ۿ�\r\n" +
                "�׿��� ķ\r\n");

            var result = AssemblyHelper.GetAssemblys().SelectMany(op => op.GetTypes())
                .ToArray()
                .Distinct();
            var abstracts = result.Where(op => !op.IsClass && op.IsAbstract && op.IsInterface).ToArray();
            // TODO: ʵ���������ڹ���Ĭ��ȫ��Scoped���ɸ��������Զ���ע�����
            foreach (var item in abstracts)
            {
                var singleResult = result.Where(op => /*op.IsClass && !op.IsAbstract && !op.IsInterface &&*/ item.IsAssignableFrom(op)).ToArray();// ע�⹤��ע��
                if (singleResult.Length > 0 && singleResult.Length < 2)
                {
                    Console.WriteLine($"Interface => { item.Name } \r\n realize => { singleResult[0].Name } \r\n");
                }
            }

            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//ע��AutoFacģ��
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()// �������л�������������һ��IHost������
                .Run();// ����
        }
    }
}
