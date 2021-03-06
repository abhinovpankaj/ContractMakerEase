//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using DragAndDropSampleManaged;

namespace DragAndDropSampleManaged
{
    public partial class MainPage : Page
    {
        public const string FEATURE_NAME = "Contract EASE";

        List<Scenario> scenarios = new List<Scenario>
        {
            new Scenario() { Title="Add new Terms & Conditions", ClassType=typeof(AddTC)},
            new Scenario() { Title="Create Contract", ClassType=typeof(CreateContract)},
            new Scenario() { Title="Approve", ClassType=typeof(ApproveTerms)}
        };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
