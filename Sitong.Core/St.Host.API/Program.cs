using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

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

            

            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory())//ע��AutoFacģ��
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .Build()// �������л�������������һ��IHost������
                .Run();// ����
        }
    }
}
