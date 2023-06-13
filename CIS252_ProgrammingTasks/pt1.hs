-- Name: Joseph Busacca Email: jebusacc@syr.edu
-- PROGRAMMING TASK 1
-- SOURCES: 
-- This work was done all by myself and I have consulted no sources other than slides and class materials

---------------------- 1 -------------------------------

-- PURPOSE
-- online (x1,y1) a b c
-- tests whether (x1,y1) is on the line ax+by+c=0

-- DEFINITION
onLine :: (Float, Float) -> Float -> Float -> Float -> Bool
onLine (x1, y1) a b c = a * x1 + b * y1 + c == 0

-- TESTS
-- TestCase1: onLine (1,2) 3 4 5      == False
-- TestCase2: onLine (0,0) 1 2 1      == False
-- TestCase3: onLine (1, -4) 1 0 (-4) == False

---------------------- 2 -------------------------------

-- PURPOSE
-- degenerate a b c
-- determines whether the equation ax+by+c=0 is degenerate

-- DEFINITION
degenerate :: Float -> Float -> Float -> Bool
degenerate a b c = a == 0 && b == 0 && c /= 0

-- TESTS
-- TestCase1: degenerate 1 2 3      = False
-- TestCase2: degenerate 0 0 1      = True
-- TestCase3: degenerate (-1) 0 4   = False

---------------------- 3 -------------------------------

-- PURPOSE
-- horizontal a b c
-- determines whether the equation ax+by+c=0 names a horizontal line. (Can assume the equation is not degenerate)

-- DEFINITION
horizontal :: Float -> Float -> Float -> Bool
horizontal a b _ = b == 0

-- TESTS
-- TestCase1: horizontal 1 2 3    = False
-- TestCase2: horizontal 1 0 (-1) = True
-- TestCase3: horizontal 8 1 2    = False

---------------------- 4 -------------------------------

-- PURPOSE
-- vertical a b c
-- determines whether the equation ax+by+c=0 names a vertical line. (Can assume the equation is not degenerate)

-- DEFINITION
vertical :: Float -> Float -> Float -> Bool
vertical a b _ = a == 0

-- TESTS
-- TestCase1: vertical 0 1 2    = True
-- TestCase2: vertical 1 1 1    = False
-- TestCase3: vertical 2 (-2) 0 = False

---------------------- 5 -------------------------------

-- PURPOSE
-- xIntercept a b c
-- returns the x-coordinate of the x-intercept of the line named by ax+by+c=0, when the line is not degenerate and not horizontal, If the line is degenerate or horizontal, return 0.0

-- DEFINITION
xIntercept :: Float -> Float -> Float -> Float
xIntercept a b c = if horizontal a b c || degenerate a b c then 0.0 else -c / a

-- TESTS
-- TestCase1: xIntercept 1 2 3    = -3.0
-- TestCase2: xIntercept (-2) 0 1 = 0.0
-- TestCase3: xIntercept 4 5 6    = -1.5

---------------------- 6 -------------------------------

-- PURPOSE
-- yIntercept a b c
-- returns the y-coordinate of the y-intercept of the line named by ax+by+c=0, when the line is not degenerate and not vertical, If the line is degenerate or vertical, return 0.0

-- DEFINITION
yIntercept :: Float -> Float -> Float -> Float
yIntercept a b c = if vertical a b c || degenerate a b c then 0.0 else -c / b

-- TESTS
-- TestCase1: yIntercept 1 2 3    = -1.5
-- TestCase2: yIntercept 0 (-2) 1 = 0.0
-- TestCase3: yIntercept 4 5 6    = -1.2

---------------------- 7 -------------------------------

-- PURPOSE
-- parallel a1 b1 c1 a2 b2 c2
-- tets whether the two lines named by the equatios a1*x + b1*x + c1 = 0 and a2*x + b2*y + c2 = 0 are parallel. (Can assume both lines are nondegenerate)

-- DEFINITION
parallel :: Float -> Float -> Float -> Float -> Float -> Float -> Bool
parallel a1 b1 _ a2 b2 _ = a1 / a2 == b1 / b2

-- TESTS
-- TestCase1: parallel 1 2 3 4 5 6  = False
-- TestCase2: parallel 0 7 3 6 2 1  = False
-- TestCase3: parallel 1 2 3 1 2 3  = True

---------------------- 8 -------------------------------

-- PURPOSE
-- intersect a1 b1 c1 a2 b2 c2
-- tests whether the two lines named by the equations a1 · x + b1 · y + c1 = 0 and a2 · x + b2 · y + c2 = 0 intersect, when neither line is degenerate. If either line is degenerate, then False is returned

-- DEFINITION
intersect :: Float -> Float -> Float -> Float -> Float -> Float -> Bool
intersect a1 b1 c1 a2 b2 c2 = not (degenerate a1 b1 c1 || degenerate a2 b2 c2) && not (parallel a1 b1 c1 a2 b2 c2)

-- TESTS
-- TestCase1: intersect 0 1 2 3 4 5    = True
-- TestCase2: intersect (-1) 2 4 8 9 1 = True
-- TestCase3: intersect 1 2 3 1 2 3    = False

---------------------- 9 -------------------------------

-- PURPOSE
-- intersectionPt a1 b1 c1 a2 b2 c2
-- returns the x-y-coordinates of the intersection point of the two lines named by the equations a1 · x + b1 · y + c1 = 0 and a2 · x + b2 · y + c2 = 0, when these lines do intersect. If the lines fail to intersect, (0.0,0.0) is returned.

-- DEFINITION
intersectionPt :: Float -> Float -> Float -> Float -> Float -> Float -> (Float, Float)
intersectionPt a1 b1 c1 a2 b2 c2
  | not (intersect a1 b1 c1 a2 b2 c2) = (0.0, 0.0)
  | otherwise = (- (b1 * c2 - b2 * c1) / (a1 * b2 - a2 * b1), - (a1 * c2 - a2 * c1) / (a1 * b2 - a2 * b1))

-- TESTS
-- TestCase1: intersectionPt 1 2 3 4 5 6    = (-1.0,-2.0)
-- TestCase2: intersectionPt (-1) 0 1 2 8 2 = (-1.0,-0.5)
-- TestCase3: intersectionPt 0 0 6 1 3 9    = (0.0,0.0)

---------------------- 10 -------------------------------

-- PURPOSE
-- lineEqual a1 b1 c1 a2 b2 c2
-- determines whether the lines named by the equations a1 · x + b1 · y + c1 = 0 and a2 · x + b2 · y + c2 = 0 are the same line. If either line is degenerate, then False is returned.

-- DEFINITION
lineEqual :: Float -> Float -> Float -> Float -> Float -> Float -> Bool
lineEqual a1 b1 c1 a2 b2 c2 = a1 / a2 == b1 / b2 && c1 / c2 == b1 / b2 && not (degenerate a1 b1 c1 || degenerate a2 b2 c2)

-- TESTS
-- TestCase1: lineEqual 0 1 2 3 4 5    = False
-- TestCase2: lineEqual 4 2 6 4 2 6    = True
-- TestCase3: lineEqual (-1) 2 3 0 4 6 = False