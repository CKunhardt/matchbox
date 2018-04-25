from trueskill import *

# Inital ratings
player1 = Rating()
player2 = Rating()
player3 = Rating()
player4 = Rating()
player5 = Rating()
player6 = Rating()
player7 = Rating()
player8 = Rating()

print("The inital ratings are: ")
print(player1.mu, player2.mu, player3.mu, player4.mu, player5.mu, player6.mu, player7.mu, player8.mu)
print("---------------------")

# Quarterfinals
player1, player2 = rate_1vs1(player1, player2)
player3, player4 = rate_1vs1(player3, player4)
player5, player6 = rate_1vs1(player5, player6)
player7, player8 = rate_1vs1(player7, player8)

print("The ratings after the quaterfinals are: ")
print(player1, player2, player3, player4, player5, player6, player7, player8)
print("---------------------")

# Semifinals
player1, player3 = rate_1vs1(player1, player3)
player5, player7 = rate_1vs1(player5, player7)

print("The ratings after the semifinals are: ")
print(player1, player2, player3, player4, player5, player6, player7, player8)
print("---------------------")

# Finals
player1, player5 = rate_1vs1(player1, player5)

print("The ratings after the finals are: ")
print(player1, player2, player3, player4, player5, player6, player7, player8)
print("---------------------")
