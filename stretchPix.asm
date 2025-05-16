stretchPix ;растягивает байт пикселей в ширину в два раза
                ;растягиваем по ширине
            ld      a,(hl)
            exx
            ld      e,a
            and     %10000000
            ld      d,a
            rrca
            or      d
            ld      d,a
            ld      a,e
            and     %01000000
            rrca
            or      d
            ld      d,a
            ld      a,e
            and     %01000000
            rrca
            rrca
            or      d
            ld      d,a
            ld      a,e
            and     %00100000
            rrca
            rrca
            or      d
            ld      d,a
            ld      a,e
            and     %00100000
            rrca
            rrca
            rrca
            or      d
            ld      d,a
            ld      a,e
            and     %00010000
            rrca
            rrca
            rrca
            or      d
            ld      d,a
            ld      a,e
            and     %00010000
            rrca
            rrca
            rrca
            rrca
            or      d
            ld      d,a
            exx
            ld      (de),a
            ;вторая половина

            inc     de
            exx
            ld      a,e
            and     %00000001
            ld      d,a
            rlca
            or      d
            ld      d,a
            ld      a,e
            and     %00000010
            rlca
            or      d
            ld      d,a
            ld      a,e
            and     %00000010
            rlca
            rlca
            or      d
            ld      d,a
            ld      a,e
            and     %00000100
            rlca
            rlca
            or      d
            ld      d,a
            ld      a,e
            and     %00000100
            rlca
            rlca
            rlca
            or      d
            ld      d,a
            ld      a,e
            and     %00001000
            rlca
            rlca
            rlca
            or      d
            ld      d,a
            ld      a,e
            and     %00001000
            rlca
            rlca
            rlca
            rlca
            or      d
            ;ld      d,a
            exx
            ld      (de),a
			ret