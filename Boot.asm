	DEVICE ZXSPECTRUM256
	SLOT 1
    	PAGE 5
	org #4000
start_boot	
	incbin "boot.B"
end__boot	
	SAVETRD "GMXPV.TRD",|"boot.B",start_boot,end__boot-start_boot	

start_GMXPVB	
	incbin "GMXPV.B"
end__GMXPVB	
	SAVETRD "GMXPV.TRD",|"GMXPV.B",start_GMXPVB,end__GMXPVB-start_GMXPVB