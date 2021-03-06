﻿using System.Collections.Generic;
using ProjectHekate.Scripting.Interfaces;

namespace ProjectHekate.Scripting.Bytecode.Emitters
{
    public class ActionDeclarationStatementEmitter : EmptyEmitter
    {        
        private readonly IReadOnlyList<string> _parameterNames;
        private readonly string _name;
        private readonly IReadOnlyList<IBytecodeEmitter> _statements;

        public ActionDeclarationStatementEmitter(IReadOnlyList<string> parameterNames, string name, IReadOnlyList<IBytecodeEmitter> statements)
        {
            _parameterNames = parameterNames;
            _name = name;
            _statements = statements;
        }

        public override void EmitTo(ICodeBlock codeBlock, IVirtualMachine vm, IScopeManager scopeManager)
        {
            var bUpdaterCodeBlock = new ActionCodeScope(_parameterNames);

            scopeManager.Add(bUpdaterCodeBlock);
            foreach (var statement in _statements)
            {
                statement.EmitTo(bUpdaterCodeBlock, vm, scopeManager);
            }
            scopeManager.Remove();

            // done, now add to the pool of action records
            vm.AddActionCodeScope(_name, bUpdaterCodeBlock);
        }
    }
}
