package day4
import "core:fmt"
import "core:encoding/varint"
import "core:math"
import "core:os"
import "core:strconv"
import "core:strings"

day: int : 4
testing: bool : false

main :: proc() {
	day_str := fmt.tprintf("%d", day)
	builder := strings.builder_make()
	strings.write_string(&builder, "2023/inputs/")
	strings.write_string(&builder, day_str)

	if testing {strings.write_string(&builder, "T.txt")} else {strings.write_string(&builder, ".txt")}
	filepath := strings.to_string(builder)

	data, ok := os.read_entire_file(filepath, context.allocator)

	if !ok {fmt.eprintfln("Error reading input of path: %s", filepath);return}
	defer delete(data, context.allocator)

	input := string(data)

	cards := parse_input(&input)
	fmt.printfln("\x1b[%dm----Day %s----\x1b[0m", 31+day%6, day_str)
	fmt.printfln("Part 1: %d", part_one(cards))
	fmt.printfln("Part 2: %d", part_two(cards))

	for c in cards {
		delete(c.winning_nums)
		delete(c.current_nums)
	}
	delete(cards)
}

card :: struct {
	winning_nums: [dynamic]int,
	current_nums: [dynamic]int,
}

parse_input :: proc(input: ^string) -> [dynamic]card {
	input_copy := input^

	cards := make([dynamic]card)
	vdfknd := 0
	for l in strings.split_lines_iterator(&input_copy) {
		vdfknd += 1
		current_card := card{}
		line := strings.split(l, ": ")[1]
		num_split := strings.split(line, " | ")

		for side, index in num_split {
			num_strs, _ := strings.replace_all(side, "  ", " ")

			space_split := strings.split(num_strs, " ")

			for num_str in space_split {
				if num_str == "" {continue}
				num, ok := strconv.parse_int(num_str, 10)


				if index == 0 {
					contains := false
					if len(current_card.winning_nums) > 0 {
						for n in current_card.winning_nums {
							if n == num {
								contains = true
								break
							}
						}
					}

					if !contains {
						append(&current_card.winning_nums, num)
					}
				} else if index == 1 {
					append(&current_card.current_nums, num)
				}
			}
		}

		append(&cards, current_card)
	}

	return cards
}

score_card :: proc(current_card: ^card) -> int {
	matches := 0

	for winning in current_card.winning_nums {
		for current in current_card.current_nums {
			if winning == current {
				matches += 1
				break
			}
		}
	}

	return matches
}
part_one :: proc(cards: [dynamic]card) -> int {
	total := 0
	for card, index in cards {
		total += 1 << uint((score_card(&cards[index]) - 1))
	}
	return total
}

part_two :: proc(cards: [dynamic]card) -> int{
	total := 0
	card_map := make(map[int]int);
	defer delete(card_map);
	for index := 0; index < len(cards); index+=1 {
		copies := 1;
		if index+1 in card_map{
			card_map[index+1] = 1;
		}else{
			card_map[index+1] += 1
			copies = card_map[index+1];
		}

		matches := score_card(&cards[index])

		for i := index; i < index + matches; i += 1 {
			card_map[i+2] += copies;
		}
	}

	for key in card_map{
		total += card_map[key]
	}
	return total
}
