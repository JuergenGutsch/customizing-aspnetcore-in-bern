using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using GenFu;
using Microsoft.Extensions.ObjectPool;
using ModelBinderSample;

namespace OutputFormatterSample.Controllers;

[ApiController]
[Route("[Controller]")]
public class PersonController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Person>> Get()
    {
        var persons = A.ListOf<Person>(25);
        return persons;
    }

    [HttpPost]
    public ActionResult<IEnumerable<Person>> Post(
        [ModelBinder(binderType: typeof(PersonsCsvBinder))]
        IEnumerable<Person> persons
    )
    {

        return persons.ToList();
    }
}


[ModelBinder(BinderType = typeof(PersonsCsvBinder))]
public class Person
{
    public int Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String EmailAddress { get; set; }
    public String City { get; set; }
    public String Address { get; set; }
    public String Phone { get; set; }
}