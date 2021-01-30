// Copyright (c) .NET Core Community. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dispatcher
{
    /// <summary>
    /// A descriptor of user definition method.
    /// </summary>
    public class ConsumerExecutorDescriptor
    {
        public TypeInfo ServiceTypeInfo { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public TypeInfo ImplTypeInfo { get; set; }
    }
}