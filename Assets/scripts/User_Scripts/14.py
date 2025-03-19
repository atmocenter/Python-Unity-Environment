import sys
def custom_exception(exc_type, exc_value, exc_traceback):
	import traceback
	print(exc_value)
	traceback.print_tb(exc_traceback)
	import Virtualenvcontroller
	Virtualenvcontroller.stop.set()
	sys.exit()
sys.excepthook = custom_exception 

#template code below  
#get the userID, automatically provided when connected to site  
import userInfo 
user_ID = userInfo.userID  
#bring cube into the environment, userID is needed 
from Virtualenvcontroller import VirtualEnvController 
environment = VirtualEnvController() 
environment.printCube('ffafff', user_ID)
import time
time.sleep(6)

import Virtualenvcontroller 
Virtualenvcontroller.stop.set()
