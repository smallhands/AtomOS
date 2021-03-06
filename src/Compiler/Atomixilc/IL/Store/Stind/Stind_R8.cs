﻿/*
* PROJECT:          Atomix Development
* LICENSE:          Copyright (C) Atomix Development, Inc - All Rights Reserved
*                   Unauthorized copying of this file, via any medium is
*                   strictly prohibited Proprietary and confidential.
* PURPOSE:          Stind_R8 MSIL
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
*/

using System;
using System.Reflection;

using Atomixilc.Machine;
using Atomixilc.Attributes;

namespace Atomixilc.IL
{
    [ILImpl(ILCode.Stind_R8)]
    internal class Stind_R8_il : MSIL
    {
        public Stind_R8_il()
            : base(ILCode.Stind_R8)
        {

        }

        /*
         * URL : https://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.Stind_R8(v=vs.110).aspx
         * Description : Stores a value of type float64 at a supplied address.
         */
        internal override void Execute(Options Config, OpCodeType xOp, MethodBase method, Optimizer Optimizer)
        {
            if (Optimizer.vStack.Count < 2)
                throw new Exception("Internal Compiler Error: vStack.Count < 2");

            /* The stack transitional behavior, in sequential order, is:
             * An address is pushed onto the stack.
             * A value is pushed onto the stack.
             * The value and the address are popped from the stack; the value is stored at the address.
             */

            var itemA = Optimizer.vStack.Pop();
            var itemB = Optimizer.vStack.Pop();

            switch (Config.TargetPlatform)
            {
                case Architecture.x86:
                    {
                        if (!itemA.SystemStack)
                            throw new Exception(string.Format("UnImplemented-RegisterType '{0}'", msIL));

                        if (!itemB.SystemStack)
                            throw new Exception(string.Format("UnImplemented-RegisterType '{0}'", msIL));

                        Stind_I_il.Executex86(8);
                    }
                    break;
                default:
                    throw new Exception(string.Format("Unsupported target platform '{0}' for MSIL '{1}'", Config.TargetPlatform, msIL));
            }

            Optimizer.SaveStack(xOp.NextPosition);
        }
    }
}
