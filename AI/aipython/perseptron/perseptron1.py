# 단층 레이어 + 게임
## 계단 함수를 통한 행동 우선 순위, 공격과 패링의 우선순위 -> 예외 처리, 움직임의 우선 순위
### 굳이 단층 레이어를 사용해야 하는가
import numpy as np

def AND(x1, x2): # theta = -bias 위의 코드 변형 형태 AND 게이트
    x = np.array([x1, x2])
    w = np.array([0.5,0.5])
    b = -0.7
    tmp = np.sum(w*x) + b
    
    if tmp <= 0:
        return 0
    else:
        return 1
    
def NAND(x1, x2): # NOT AND 게이트
    x = np.array([x1, x2])
    w = np.array([-0.5, -0.5])
    b = 0.7
    tmp  = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1
    
def OR(x1, x2): # OR 게이트
    x = np.array([x1, x2])
    w = np.array([0.5, 0.5])
    b = -0.2
    tmp = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else:
        return 1

#XOR 직접 구현해보고 예제와 비교
## XOR 설명
#  0 0 => 0
#  1 0 => 1
#  0 1 => 1
#  1 1 => 0
# bias, 가중치 설정
def XOR(x1, x2):
    x = np.array([x1, x2])
    w = np.array([0, 0])
    b = 0
    tmp = np.sum(w*x) + b
    if tmp <= 0:
        return 0
    else: 
        return 1