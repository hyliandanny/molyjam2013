using System;
using System.Collections.Generic;

public delegate void HandleMessage(Message message);
// One parameter
static public class Messenger {
	private static Dictionary<Type, Delegate> eventTable = new Dictionary<Type, Delegate>();
 
	static public void AddListener(Type messageType, HandleMessage handler) {
		lock(eventTable) {
			if(!eventTable.ContainsKey(messageType)) {
				eventTable.Add(messageType,null);
			}
			eventTable[messageType] =  (HandleMessage)eventTable[messageType] + handler;
		}
	}
 
	static public void RemoveListener(Type messageType, HandleMessage handler) {
		lock(eventTable) {
			if(eventTable.ContainsKey(messageType)) {
				eventTable[messageType] = (HandleMessage)eventTable[messageType] - handler;
			}
		}
	}
 
	static public void Invoke(Type messageType, Message message)
    {
		Delegate d;
        // Invoke the delegate only if the event type is in the dictionary.
        if (eventTable.TryGetValue(messageType, out d))
        {
			HandleMessage handler = (HandleMessage)d;
            if (handler != null)
            {
                handler(message);
            }
        }
    }
}