using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public abstract class CommandInvoker : MonoBehaviour
    {
        private static Stack<ICommand> _undoStack = new();

        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
        }

        public static void UndoCommand()
        {
            if (_undoStack.Count > 0)
            {
                ICommand activeCommand = _undoStack.Pop();
                activeCommand.Undo();
            }
        }
    }
}