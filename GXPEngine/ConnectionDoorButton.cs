using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class ConnectionDoorButton
    {
        public ButtonDoor button=null;
        public Door door=null;



        public void Connect()
        { 
            if(button!=null && door!=null)
            door.buttonDoor = button;
        }
    }
}
