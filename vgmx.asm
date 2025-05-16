vgmx  ;load and view pic GMX
            call   readfile2n
            ld     a,firstpag
            call   pag_on
            ld     hl,#8000 ;copy pic
            ld     de,#c000
            ld     bc,16000
            ldir

            ; ld a,0
            ; call   pag_on
            call   readfile2n
			
            ; ld     hl,#c000 ;copy attr
            ; ld     de,#8000
            ; ld     bc,16000
            ; ldir
            ld     a,firstpag
            inc    a
            call   pag_on
            ld     hl,#8000+256 ;copy atr
            ld     de,#c000
            ld     bc,16000
            ldir

            call   showpic
            ld     a,2
            ld     (allpicpag),a
            ret