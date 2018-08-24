﻿using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class EditAudiobookViewModel
    {
        public Audiobook Audiobook { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool? Confirmation { get; set; }
    }
}