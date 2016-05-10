<?php


header ("Content-Type:text/css");


/** ===============================================================
 *
 *      Edit your Color Configurations below:
 *      You should only enter 6-Digits HEX Colors.
 *
 ================================================================== */


$color = "#57B3DF"; // Change your Color Here


/** ===============================================================
 *
 *      Do not Edit anything below this line if you do not know
 *      what you are trying to do..!
 *
 ================================================================== */


function checkhexcolor($color) {

    return preg_match('/^#[a-f0-9]{6}$/i', $color);

}


/** ===============================================================
 *
 *      Primary Color Scheme
 *
 ================================================================== */


if( isset( $_GET[ 'color' ] ) AND $_GET[ 'color' ] != '' ):

    $color = "#" . $_GET[ 'color' ];
    
endif;

if( !$color OR !checkhexcolor( $color ) ) {
    
    $color = "#57B3DF";
    
}


?>


/* ----------------------------------------------------------------
    Colors
-----------------------------------------------------------------*/


footer a:hover.feat article p a  { color: <?php echo $color; ?>; }


/* ----------------------------------------------------------------
    Background Colors
-----------------------------------------------------------------*/

.horizontal-nav li a:hover , .view-thumb a.info{ background-color: <?php echo $color; ?>; }
{ background-color: <?php echo $color; ?> !important; }

{ background-color: rgba(11,11,11,0.8) !important; }


/* ----------------------------------------------------------------
    Border Colors
-----------------------------------------------------------------*/


.footbg, footer{ border-color: <?php echo $color; ?>; }
{ border-top-color: <?php echo $color; ?>; }
{ border-bottom-color: <?php echo $color; ?>; }
{ border-left-color: <?php echo $color; ?>; }
{ border-left-color: <?php echo $color; ?> !important; }


/* ----------------------------------------------------------------
    Selection Colors
-----------------------------------------------------------------*/


::selection,
::-moz-selection,
::-webkit-selection { background-color: <?php echo $color; ?>; }