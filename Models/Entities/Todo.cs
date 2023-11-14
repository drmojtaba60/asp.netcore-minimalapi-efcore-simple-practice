namespace ToDoMinimalApiContextDbPractice.Models.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }=false;

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public virtual ICollection<Todo> Todoes { get; set; }=new HashSet<Todo>();
    }
}
