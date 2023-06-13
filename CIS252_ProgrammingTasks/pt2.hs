-- Name: Joseph Busacca Email: jebusacc@syr.edu
-- PROGRAMMING TASK 2
-- SOURCES: 
-- This work was done all by myself and I have consulted no sources other than slides and class materials


type Bag a = [(a,Integer)]

-- following bags must:
-- no bag contains two pairs that have the same first component
-- [('a',2),('x',5),('a',1)] == INVALID
-- [('a',2),('x',5)] == VALID
-- no bag contains a pair with a number smaller than 1
-- [('a',0),('x',5)] == INVALID

---------------------- 1 -------------------------------

charBag1 :: Bag Char
charBag1 = [('e',1), ('y',2), ('z',1)]

charBag2 :: Bag Char
charBag2 = [('e',4), ('y',2), ('z',3), ('j',2)]

charBag3 :: Bag Char
charBag3 = [('y',3), ('j',1)]

stringBag1 :: Bag String
stringBag1 = [("juice",2), ("cereal",1), ("toothpaste",3), ("soup",1)]

nonBag :: [(String,Integer)]
nonBag = [("a",1), ("a",2), ("x",-1)]

---------------------- 2 -------------------------------
-- PURPOSE
-- such that totalItems bag calculates the total number of items in bag

-- DEFINITION
totalItems :: Bag a -> Integer
totalItems [] = 0
totalItems ((_,n):xs) = n + totalItems xs

-- TESTS
-- totalItems charBag1      = 4
-- totalItems stringBag1    = 7
-- one tests a charbag, other tests a stringbag
---------------------- 3 -------------------------------
-- PURPOSE
-- such that howMany item bag calculates the number of copies of item in bag

-- DEFINITION
howMany :: Eq a => a -> Bag a -> Integer
howMany _ [] = 0
howMany item ((x,n):xs)
    | item == x = n
    | otherwise = howMany item xs

-- TESTS
-- howMany 'e' charBag2         = 4
-- howMany "juice" stringBag1   = 2
-- tests a charbag and a stringbag of different values

---------------------- 4 -------------------------------
-- PURPOSE
--such that addBag item bag creates the bag obtained from bag by adding one additional copy of item

-- DEFINITION
addToBag :: Eq a => a -> Bag a -> Bag a
addToBag item [] = [(item,1)]
addToBag item ((x,n):xs)
    | item == x = (x,n+1):xs 
    | otherwise = (x,n) : addToBag item xs

-- TESTS
-- addToBag 'z' charBag3        = [('y',3),('j',1),('z',1)]
-- addToBag "cereal" stringBag1 = [("juice",2), ("cereal",2), ("toothpaste",3), ("soup",1)]
-- test a charbag and a stringbag, one adds a new element to the bag, the other adds an already exisiting element to the bag

---------------------- 5 -------------------------------
-- PURPOSE
-- such that removeFromBag item bag creates the bag obtained by removing one copy of item from bag

-- DEFINITION
removeFromBag :: Eq a => a -> Bag a -> Bag a
removeFromBag item [] = []
removeFromBag item ((x,n):xs)
    | item == x && n > 1 = (x,n-1):xs 
    | item == x && n == 1 = xs 
    | otherwise = (x,n) : removeFromBag item xs

-- TESTS
-- removeFromBag 'z' charBag2           = [('e',4),('y',2),('z',2),('j',2)]
-- removeFromBag "cereal" stringBag1    = [("juice",2), ("toothpaste",3), ("soup",1)]
-- tests a charbag and a stringbag, decrements an element by 1 but leaves it in, other on removes the item from the bag entirely

---------------------- 6 -------------------------------
-- PURPOSE
-- such that createBagFrom str creates a bag that contains all of the characters from str

-- DEFINITION
createBagFrom :: String -> Bag Char
createBagFrom [] = []
createBagFrom (c:cs) = addToBag c (createBagFrom cs)

-- TESTS
-- createBagFrom "syracuse"         = [('e',1),('s',2),('u',1),('c',1),('a',1),('r',1),('y',1)]
-- createBagFrom "otto the orange"  = [('e',2),('g',1),('n',1),('a',1),('r',1),('o',3),(' ',2),('h',1),('t',3)]
-- test cases have reoccuring letters and spaces, one has multiple words

---------------------- 7 -------------------------------
-- PURPOSE
-- such that subBag bag1 bag2 determines whether bag1 is a subbag of bag2

-- DEFINITION
subBag :: Eq a => Bag a -> Bag a -> Bool
subBag [] _ = True
subBag ((x,n):xs) bag = (n <= howMany x bag) && subBag xs bag

-- TESTS
-- subBag charBag3 charBag1     = False
-- subBag charBag2 charBag2     = True
-- tests a bag that isnt a sub bag, tests a bag that is itself, and the only other True case was used in the lab instructrions

---------------------- 8 -------------------------------
-- PURPOSE
-- such that validBag bag determines whether bag satisfies the bag invariants

-- DEFINITION
validBag :: Eq a => Bag a -> Bool
validBag [] = True
validBag ((x,n):xs) = (n > 0) && notElem x (map fst xs) && validBag xs

-- TESTS
-- validBag charBag2        = True
-- validBag stringBag1      = True
-- validBag nonBag          = False
-- tests a charbag, a stringbag, and the nonbag