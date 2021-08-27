using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Base_SP
    {
        int id;
        bool isActive;
        string createdDate;
        string lastModifiedDate;

        public int Id { get => id; set => id = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string CreatedDate { get => createdDate; set => createdDate = value; }
        public string LastModifiedDate { get => lastModifiedDate; set => lastModifiedDate = value; }
    }

    public class GetCategory : Base_SP
    {
        string name;

        public string Name { get => name; set => name = value; }
    }

    public class GetPost : Base_SP
    {
        int categoryId;
        string category;
        string content;
        String postingDate;
        string title;

        public int CategoryId { get => categoryId; set => categoryId = value; }
        public string Category { get => category; set => category = value; }
        public string Content { get => content; set => content = value; }
        public String PostingDate { get => postingDate; set => postingDate = value; }
        public string Title { get => title; set => title = value; }
    }
}
