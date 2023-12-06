using System.ComponentModel.DataAnnotations;
using WebApi.Data;

namespace WebApi.Models
{
    public class Lesson : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}

//public static class LessonEndpoints
//{
//	public static void MapLessonEndpoints (this IEndpointRouteBuilder routes)
//    {
//        routes.MapGet("/api/Lesson", async (WebDbContext db) =>
//        {
//            return await db.Lessons.ToListAsync();
//        })
//        .WithName("GetAllLessons")
//        .Produces<List<Lesson>>(StatusCodes.Status200OK);

//        routes.MapGet("/api/Lesson/{id}", async (Guid Id, WebDbContext db) =>
//        {
//            return await db.Lessons.FindAsync(Id)
//                is Lesson model
//                    ? Results.Ok(model)
//                    : Results.NotFound();
//        })
//        .WithName("GetLessonById")
//        .Produces<Lesson>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);

//        routes.MapPut("/api/Lesson/{id}", async (Guid Id, Lesson lesson, WebDbContext db) =>
//        {
//            var foundModel = await db.Lessons.FindAsync(Id);

//            if (foundModel is null)
//            {
//                return Results.NotFound();
//            }

//            db.Update(lesson);

//            await db.SaveChangesAsync();

//            return Results.NoContent();
//        })
//        .WithName("UpdateLesson")
//        .Produces(StatusCodes.Status404NotFound)
//        .Produces(StatusCodes.Status204NoContent);

//        routes.MapPost("/api/Lesson/", async (Lesson lesson, WebDbContext db) =>
//        {
//            db.Lessons.Add(lesson);
//            await db.SaveChangesAsync();
//            return Results.Created($"/Lessons/{lesson.Id}", lesson);
//        })
//        .WithName("CreateLesson")
//        .Produces<Lesson>(StatusCodes.Status201Created);


//        routes.MapDelete("/api/Lesson/{id}", async (Guid Id, WebDbContext db) =>
//        {
//            if (await db.Lessons.FindAsync(Id) is Lesson lesson)
//            {
//                db.Lessons.Remove(lesson);
//                await db.SaveChangesAsync();
//                return Results.Ok(lesson);
//            }

//            return Results.NotFound();
//        })
//        .WithName("DeleteLesson")
//        .Produces<Lesson>(StatusCodes.Status200OK)
//        .Produces(StatusCodes.Status404NotFound);
//    }
//}}
