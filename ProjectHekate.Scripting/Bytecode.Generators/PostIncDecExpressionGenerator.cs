﻿using System;
using ProjectHekate.Scripting.Bytecode.Emitters;
using ProjectHekate.Scripting.Interfaces;

namespace ProjectHekate.Scripting.Bytecode.Generators
{
    public class PostIncDecExpressionGenerator : EmptyEmitter
    {
        private readonly IdentifierType _identifierType;
        private readonly string _identifierName;
        private readonly Instruction _op;

        public PostIncDecExpressionGenerator(IdentifierType identifierType, string identifierName, Instruction op)
        {
            // basic validation of operator
            switch (op)
            {
                case Instruction.OpAdd:
                case Instruction.OpSubtract:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("op", op, "The operator must be either the addition operator or the subtraction operator.");
            }

            _identifierType = identifierType;
            _identifierName = identifierName;
            _op = op;
        }
        
        public override ICodeBlock Generate(IVirtualMachine vm, IScopeManager scopeManager)
        {           
            // Post-inc/decrement expression code:
            // Instruction.GetVariable/Property
            // {index of variable/property}
            // Instructions.Push
            // {1}
            // Instruction.OpAdd/Subtract
            // Instruction.SetVariable/Property
            // {index of variable/property}

            var code = new CodeBlock();

            var oneLitGen = new LiteralExpressionGenerator(1);
            var compAssignExprGen = new CompoundAssignmentExpressionGenerator(oneLitGen, _identifierType, _identifierName, _op);

            code.Add(compAssignExprGen.Generate(vm, scopeManager));

            return code;
        }
    }
}
