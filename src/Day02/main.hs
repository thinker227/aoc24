input = "7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\n8 6 4 4 1\n1 3 6 7 9"

main :: IO ()
main = let x = parse input in putStrLn $
       ("Part 1: " ++ run check x) ++ "\n" ++
       ("Part 2: " ++ run check2 x)

parse :: String -> [[Int]]
parse s = (map read . words) <$> lines s

run f = show . length . filter f

check x = isSafe id x || isSafe negate x  -- part 1
check2 x = or $ map check (mut x)         -- part 2

isSafe f xs = all (\(a, b) -> f (a - b) `elem` [1..3]) (group xs)

mut xs = map (rm xs) [1..length xs]

rm xs n = take (n - 1) xs ++ drop n xs

group (a:b:xs) = (a, b) : group (b : xs)
group _ = []
