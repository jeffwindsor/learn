# Uses python3
import sys

def fib(n):
    if (n < 2):
        return n
    else:
        a = 0
        b = 1
        t = 0
        for x in range(2, n + 1):
            t = a + b
            #print(x," : ",a," ",b," ",t)
            a = b
            b = t
        return b

def fh(n, m):
    nprime = n % fib(m*2)
    return nprime
    #return fib(nprime) % m

if __name__ == '__main__':
    input = sys.stdin.read();
    n, m = map(int, input.split())
    print(fh(n, m))
