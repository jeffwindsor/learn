# Uses python3
import sys

def get_majority_element(a, n):
    if (n == 1):
        return a[0]

    mid = n // 2
    l = get_majority_element(a[:mid], mid)
    r = get_majority_element(a[mid:], n - mid)

    if(l == r) or (a.count(l) > mid):
        return l
    if(a.count(r) > mid):
        return r

    return -1

if __name__ == '__main__':
    input = sys.stdin.read()
    n, *a = list(map(int, input.split()))
    if get_majority_element(a, n) != -1:
        print(1)
    else:
        print(0)
