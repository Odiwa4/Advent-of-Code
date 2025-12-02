package main;
import "day1";
import "day2";

import "day4";
import "day6";
import "core:fmt";
import "core:strings"

main :: proc(){
	pretty_print(string("ADVENT OF CODE 2023"))

	day1.main();
	day2.main();
	day4.main();
	day6.main();
}

pretty_print :: proc(message: string){
	copy := message
	i := 0;
	for letter in strings.split_iterator(&copy, " "){
		i += 1;
		fmt.printf("\x1b[%dm%s\x1b[0m", 32 + i % 4, letter)
		fmt.printf(" ")
	}
	fmt.print("\n")
}