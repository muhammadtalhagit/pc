using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentEmail { get; set; } = null!;

    public int StudentPhone { get; set; }

    public string StudentImages { get; set; } = null!;

    public int TrainerId { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Trainer Trainer { get; set; } = null!;
}
