using System;

namespace ChuckItApi.Models
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public int MyProperty { get; set; }

        public DateTime DateTime { get; set; }
        public string Message { get; set; }
        public UserDto FromUser { get; set; }
        public UserDto ToUser { get; set; }

    }
}
