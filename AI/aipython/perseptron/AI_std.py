import numpy as np

def AND(x1, x2):
    w1, w2, theta = 0.5, 0.5, 0.7
    tmp = x1*w1  + x2+w2
    if tmp <= theta:
        return 0
    elif tmp > theta:
        return 1
    

def AND2(x1, x2): # theta = -bias 위의 코드 변형 형태
    x = np.array([x1, x2])
    w = np.array([0.5,0.5])
    b = -0.7
    tmp = np.sum(w*x) + b
    
    if tmp <= 0:
        return 0
    else:
        return 1
print(AND(1, 1)) #둘다 1일때만 
print(AND2(1, 1)) # 1이하의 값만 입력

def NAND(x1, x2):
    x = np.array([x1, x2])
    w = np.array([-0.5, -0.5])
    b = 0.7
    tmp  = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1
    
def OR(x1, x2):
    x = np.array([x1, x2])
    w = np.array([0.5, 0.5])
    b = -0.2
    tmp = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1