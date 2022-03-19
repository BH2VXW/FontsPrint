using Biz126.BlazorUI.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using log4net;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(cfg =>
{
    cfg.AddLog4Net();
    //Ĭ�ϵ������ļ�·�����ڸ�Ŀ¼�����ļ���Ϊlog4net.config
    //����ļ�·���������б仯����Ҫ����������·��������
    //��������Ŀ��Ŀ¼�´���һ����Ϊcfg���ļ��У���log4net.config�ļ��������У�������Ϊlog.config
    //����Ҫʹ������Ĵ�������������
    //cfg.AddLog4Net(new Log4NetProviderOptions()
    //{
    //    Log4NetConfigFileName = "cfg/log.config",
    //    Watch = true
    //});
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<FontsServeice>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
