white:
    mov     $0, 128
    mov     $1, 5
    lsh     $0, $1
    mov     $2, 255
    mov     $3, 0
    mov     [$0], $2
    mov     $10, 0
    mov     $11, 255
    jump    whiter
whiter:
    inc     $0
    inc     $10
    mov     [$0], $2
    cmp     $10, $11
    je      black
    jump    white
black:
    mov     $0, 128
    mov     $1, 5
    lsh     $0, $1
    mov     [$0], $3
    mov     $10, 0
    jump    blacker
blacker:
    inc     $0
    inc     $10
    mov     [$0], $3
    cmp     $10, $11
    je      white
    jump    blacker
