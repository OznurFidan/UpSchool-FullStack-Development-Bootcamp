using Microsoft.AspNetCore.SignalR.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Collections.Specialized.BitVector32;

IWebDriver driver = new ChromeDriver();



var hubConnection = new HubConnectionBuilder()
    .WithUrl($"https://localhost:7196/Hubs/SeleniumLogHub")
    .WithAutomaticReconnect()
    .Build();

await hubConnection.StartAsync();


driver.Navigate().GoToUrl("https://4teker.net/");


Thread.Sleep(1000);
var pagecount = 0;


IReadOnlyCollection<IWebElement> pages = driver.FindElements(By.CssSelector(".page-link.page-number"));
foreach (IWebElement page in pages)
{
    pagecount++;

}


for (int i = 0; i < pagecount; i++)
{

    IReadOnlyCollection<IWebElement> products = driver.FindElements(By.CssSelector(".card.h-100"));


    foreach (IWebElement product in products)
    {
        var productName = product.FindElement(By.ClassName("product-name")).Text;
        Console.WriteLine(productName);
        var picture = driver.FindElement(By.ClassName("card-img-top")).GetAttribute("src");
        var price = driver.FindElement(By.ClassName("price"));
        try
        {
            var salePrice = driver.FindElement(By.ClassName("sale-price"));
            var isOnSale = true;

        }
        catch (Exception)
        {
            var isOnSale = false;
            break;
        }

    }
    try
    {
        IWebElement nextPage = driver.FindElement(By.ClassName("next-page"));
        var nextPageUrl = nextPage.GetAttribute("href");
        driver.Navigate().GoToUrl(nextPageUrl);

    }
    catch (Exception)
    {

        break;
    }


    Thread.Sleep(2500);
}