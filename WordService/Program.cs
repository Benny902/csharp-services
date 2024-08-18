using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IWordSelector, WordSelector>();

var app = builder.Build();

app.MapGet("/select-word", (string commonChar, IWordSelector wordSelector) =>
{
    var words = new[] { "apple", "banana", "grape", "orange", "pear" };
    var selectedWords = wordSelector.SelectWords(words, commonChar);
    return Results.Ok(selectedWords);
});

app.Run();

public interface IWordSelector
{
    IEnumerable<string> SelectWords(IEnumerable<string> words, string commonChar);
}

public class WordSelector : IWordSelector
{
    public IEnumerable<string> SelectWords(IEnumerable<string> words, string commonChar)
    {
        return words.Where(word => word.Contains(commonChar));
    }
}