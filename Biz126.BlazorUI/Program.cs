using Biz126.BlazorUI.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using log4net;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(cfg =>
{
    cfg.AddLog4Net();
    //默认的配置文件路径是在根目录，且文件名为log4net.config
    //如果文件路径或名称有变化，需要重新设置其路径或名称
    //比如在项目根目录下创建一个名为cfg的文件夹，将log4net.config文件移入其中，并改名为log.config
    //则需要使用下面的代码来进行配置
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
