setting4096reg0:
    mov     $0, 128
    mov     $1, 5
    lsh     $0, $1
    mov     $10, 0
    mov     $11, 255
whiteRow:
    mov     $1, 255
    mov     $2, 8
    lsh     $1, $2
    mov     $2, 255
    add     $1, $2
    mov     [$0], $1
loop:
    inc     $0
    mov     [$0], $1
    jump    loop
