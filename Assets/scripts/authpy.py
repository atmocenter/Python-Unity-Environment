import sys
import crypt

password = sys.argv[1] # input('Enter password: ')
salt     = sys.argv[2] # input('Enter salt: ')
rounds   = sys.argv[3] # input('Enter rounds: ')



roundsSalt = f"$6$rounds={rounds}${salt}$"
print(crypt.crypt(password, roundsSalt))
