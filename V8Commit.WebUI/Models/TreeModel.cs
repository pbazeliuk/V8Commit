using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V8Commit.WebUI.Models
{
    public class TreeModel
    {
        public string File { get; set; }
        public List<TreeModel> Children { get; set; } 
    }
}