using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class ConnectionDoorButton
    {
        public List<DoorButton> buttons = new List<DoorButton>();
        public List<Door> doors = new List<Door>();



        public void Connect()
        {
            foreach (DoorButton button in buttons)
            {
                foreach(Door door in doors)
                {
                    if(button.buttonNumber == door.doorNumber)
                    {
                        door.button = button;
                    }
                }
            }
        }
    }
}
