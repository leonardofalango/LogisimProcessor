nop         0000 0000 0000 0000

add         0000 1000 aaaa bbbb
sub         0000 1001 aaaa bbbb
mul         0000 1010 aaaa bbbb
div         0000 1011 aaaa bbbb
lsh         0000 1100 aaaa bbbb

rsh         0000 1101 aaaa bbbb
and         0000 1111 aaaa bbbb

or          0001 0000 aaaa bbbb
not         0001 0001 aaaa bbbb
xor         0001 0010 aaaa bbbb
nand        0001 0011 aaaa bbbb
load        0001 0100 xxxx aaaa
store       0001 0101 xxxx aaaa
cmp         0001 1001 aaaa bbbb
je          1010 ffff ffff ffff
jmp         1011 ffff ffff ffff
jg          1100 ffff ffff ffff
jge         1101 ffff ffff ffff
call        1110 ffff ffff ffff
mov         1111 aaaa cccc cccc
movconst    0001 0111 pppp pppp
movi        0001 aaaa const xxxx



op 14 -> inc
op 15 -> dec