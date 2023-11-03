using Domain.OneOfTypes;
using Domain.ToDoItem;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ToDoItems.Update.Details;

public sealed record UpdateToDoItemDetailsRequest(TimeOnly StartTime,
    TimeOnly EndTime,
    string Description);