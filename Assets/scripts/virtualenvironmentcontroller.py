import threading
stop = threading.Event()
import traceback

class VirtualEnvController():
    def __init__(self):
        import queue
        import time
        import socket
        import threading
        import json
        self.msgQueue = queue.Queue()
        self.respQueue = queue.Queue()
        newThread = threading.Thread(target=self.tcpReqLoop)
        newThread.start()

    def checkStop(self):
        global stop
        stop.set()

    def printCube(self,name,id):
        import queue
        import socket
        import threading
        import json
        try:
            #check variables
            if(isinstance(name,str) == False):    
                raise ValueError("The name argument is meant to be a string")
            if(isinstance(id,int) == False):
                raise ValueError(" The id argument is meant to be a integer value")
            msgDict = {"Method": "PrintCube","args":[name,str(id)] }
            msgJson = json.dumps(msgDict)
            # print(msgJson)
            self.msgQueue.put(msgJson)
            while(True):
                if(self.respQueue.empty()==True):
                    continue
                else:
                    break
            resp = self.respQueue.get()
            if(resp == "success"):
                return resp
            else:
                # print(resp)
                
                raise Exception(resp)
                self.checkStop()
                exit()
        except Exception as e:
            # print(f"ValueError: {e}")
            traceback.print_exc()
            self.checkStop()
            raise
            exit()
            # raise Exception("fafafa")
        
    
    def applyForce(self,name,connID,x,y,z):
        import json
        import time
        try:
            if(isinstance(name,str) == False):
                raise ValueError("The name argument is meant to be a string")
            if(isinstance(connID,int) == False):
                raise ValueError("The connectionID argument is meant to be an int")
            if(isinstance(x,float) == False and isinstance(x,int)== False):
                raise ValueError(" The x argument is meant to be a number value")
            if(isinstance(z,float) == False  and isinstance(z,int)== False):
                raise ValueError(" The z argument is meant to be a number value")
            if(isinstance(y,float) == False  and isinstance(y,int)== False):
                raise ValueError(" The x argument is meant to be a number value")
            
            msgDict = {"Method": "applyForce","args":[name,str(connID),str(x), str(y),str(z)] }
            msgJson = json.dumps(msgDict)
            # print(msgJson)
            self.msgQueue.put(msgJson)
            while(True):
                if(self.respQueue.empty()==True):
                    continue
                else:
                    break
            resp = self.respQueue.get()
            if(resp == "success"):
                time.sleep(0.2)
                return resp
            else:
                print(resp)
                raise Exception(resp)
                self.checkStop()
                exit()
        except Exception as e:
            # print(f"ValueError: {e}")
            traceback.print_exc()
            self.checkStop()
            raise
            exit()
    def DeleteObj(self,name,connID):
        import json
        try:
            if(isinstance(name,str) == False):
                raise ValueError("The name argument is meant to be a string")
            if(isinstance(connID,int) == False):
                raise ValueError("The connectionID argument is meant to be an int")
        
            msgDict = {"Method": "DeleteObj","args":[name,str(connID)] }
            msgJson = json.dumps(msgDict)
            # print(msgJson)
            self.msgQueue.put(msgJson)
            while(True):
                if(self.respQueue.empty()==True):
                    continue
                else:
                    break
            resp = self.respQueue.get()
            if(resp == "success"):
                return resp
            else:
                print(resp)
                raise Exception(resp)
                self.checkStop()
                exit()
        except Exception as e:
            # print(f"ValueError: {e}")
            traceback.print_exc()
            self.checkStop()
            raise
            exit()
    

    def MoveObj(self,name,connID,x,y,z):
        import json
        try:
            if(isinstance(name,str) == False):
                raise ValueError("The name argument is meant to be a string")
            if(isinstance(connID,int) == False):
                raise ValueError("The connectionID argument is meant to be an int")
            if(isinstance(x,float) == False and isinstance(x,int)== False):
                raise ValueError(" The x argument is meant to be a number value")
            if(isinstance(z,float) == False  and isinstance(z,int)== False):
                raise ValueError(" The z argument is meant to be a number value")
            if(isinstance(y,float) == False  and isinstance(y,int)== False):
                raise ValueError(" The x argument is meant to be a number value")
            
            msgDict = {"Method": "MoveObj","args":[name,str(connID),str(x), str(y),str(z)] }
            msgJson = json.dumps(msgDict)
            # print(msgJson)
            self.msgQueue.put(msgJson)
            while(True):
                if(self.respQueue.empty()==True):
                    continue
                else:
                    break
            resp = self.respQueue.get()
            if(resp == "success"):
                return resp
            else:
                print(resp)
                raise Exception(resp)
                self.checkStop()
                exit()
        except Exception as e:
            # print(f"ValueError: {e}")
            traceback.print_exc()
            self.checkStop()
            raise
            exit()
    
    def ChangeColor(self,name,connID,r,g,b):
        import json
        try:
            if(isinstance(name,str) == False):
                raise ValueError("The name argument is meant to be a string")
            if(isinstance(connID,int) == False):
                raise ValueError("The connectionID argument is meant to be an int")
            if(isinstance(r,float) == False and isinstance(r,int)== False):
                raise ValueError(" The r argument is meant to be a number value")
            if(isinstance(g,float) == False  and isinstance(g,int)== False):
                raise ValueError(" The g argument is meant to be a number value")
            if(isinstance(b,float) == False  and isinstance(b,int)== False):
                raise ValueError(" The b argument is meant to be a number value")
            msgDict = {"Method": "ChangeColor","args":[name,str(connID),str(r), str(g),str(b)] }
            msgJson = json.dumps(msgDict)
            # print(msgJson)
            self.msgQueue.put(msgJson)
            while(True):
                if(self.respQueue.empty()==True):
                    continue
                else:
                    break
            resp = self.respQueue.get()
            if(resp == "success"):
                return resp
            else:
                raise Exception(resp)
                self.checkStop()
                exit()
        except Exception as e:
            # print(f"ValueError: {e}")
            traceback.print_exc()
            self.checkStop()
            raise
            exit()
    


    def RotateObj(self,name,connID,x,y,z):
        import json
        try:
            if(isinstance(name,str) == False):
                raise ValueError("The name argument is meant to be a string")
            if(isinstance(connID,int) == False):
                raise ValueError("The connectionID argument is meant to be an int")
            if(isinstance(x,float) == False and isinstance(x,int)== False):
                raise ValueError(" The x argument is meant to be a number value")
            if(isinstance(z,float) == False  and isinstance(z,int)== False):
                raise ValueError(" The z argument is meant to be a number value")
            if(isinstance(y,float) == False  and isinstance(y,int)== False):
                raise ValueError(" The x argument is meant to be a number value")
            msgDict = {"Method": "Rotate","args":[name,str(connID),str(x), str(y),str(z)] }
            msgJson = json.dumps(msgDict)
            # print(msgJson)
    
            self.msgQueue.put(msgJson)
            while(True):
                if(self.respQueue.empty()==True):
                    continue
                else:
                    break
            resp = self.respQueue.get()
            if(resp == "success"):
                return resp
            else:
                # print(resp)
                raise Exception(resp)
                self.checkStop()
                exit()
        except Exception as e:
            # print(f"ValueError: {e}")
            traceback.print_exc()
            self.checkStop()
            raise
            exit()
    
    
    def sendMessage(self,message):
        self.msgQueue.put(message)
        while(True):
            if(self.respQueue.empty()==True):
                continue
            else:
                break
        resp = self.respQueue.get()
        return resp

    def tcpReqLoop(self):
        import queue
        import socket
        global stop
        try:
            # print("started")
            # Create a TCP socket
            #socket.AF_INET
            client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

        
            client_socket.connect(("host.docker.internal", 5000))


            #loop through many messages, if message isn't there wait for one to be there. so while true loop...
            # Send a message
            while(stop.is_set()==False):
                if(self.msgQueue.empty()==True):
                    try:
                        # print("empty message")
                        continue
                    except KeyboardInterrupt:
                        break
                
                message = self.msgQueue.get()
                # print("newmessage")
                client_socket.sendall(message.encode('utf-8'))

                # Receive a response
                response = client_socket.recv(1024)  # Buffer size is 1024 bytes
                self.respQueue.put(response.decode('utf-8'))
                # print('Received:', response.decode('utf-8'))
        except KeyboardInterrupt:
            print("keyboard interupt")
            
        except Exception as e:
            print('Error:', e)
            self.respQueue.put(e)

        finally:
            # Close the socket
            client_socket.close()
    


# x = VirtualEnvController()
# x.printCube("ffa",0)
# print("daf")